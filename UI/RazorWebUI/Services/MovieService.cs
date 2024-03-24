using Domain.DTOs;
using Domain.Models;

namespace RazorWebUI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("api/movie");
                return movies.OrderByDescending(x=>x.SearchDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Movie>> SaveTopFiveSearch(string title, int page)
        {
            try { 
            // Send the HTTP request to get the search results
            HttpResponseMessage response = await _httpClient.GetAsync($"api/movie/savetopfivesearch?title={title}&page={page}");

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Deserialize the response content into MovieSearchResultDto
            MovieSearchResultDto searchResultDto = await response.Content.ReadFromJsonAsync<MovieSearchResultDto>();

            // Check if the search results are available
            if (searchResultDto != null && searchResultDto.Response == "True" && searchResultDto.Search != null)
            {
                // Convert the array of Search objects to a list of Movie objects
                List<Movie> searchResults = searchResultDto.Search
                    .Select(search => new Movie
                    {
                        Title = search.Title,
                        Year = search.Year,
                        ImdbId = search.imdbID,
                        Type = search.Type,
                        PosterUrl = search.Poster
                    })
                    .ToList();

                // Return the list of movies 
                return searchResults;
            }
            else
            {
                // If no search results are available or if the response is not successful, return null
                return null;
            }
            }catch(Exception c)
            {
                return null;
            }
        }

        public async Task<MovieDetailsDto> GetMovieById(string id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/omdbquery/getmoviebyid/{id}");

                response.EnsureSuccessStatusCode(); // Throws exception if not successful

                MovieDetailsDto movie = await response.Content.ReadFromJsonAsync<MovieDetailsDto>();

                return movie;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while fetching movie by ID", ex);
            }
        }


    }
}
