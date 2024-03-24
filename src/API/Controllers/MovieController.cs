using Application.Commands.MovieCommand;
using Application.Queries.MovieQueries;
using Application.Queries.OmdbQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            movie.SearchDate = DateTime.UtcNow.AddHours(1);
            AddMovieCommand command = new AddMovieCommand(movie);
            await _mediator.Send(command);

            return Ok("Movie added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(id);
            await _mediator.Send(command);

            return Ok("Movie deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(long id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("The provided movie ID does not match the ID in the request body.");
            }

            var command = new UpdateMovieCommand(movie);
            await _mediator.Send(command);

            return Ok("Movie updated successfully");
        }

        [HttpGet]
        public async Task<IActionResult> ListMovies()
        {
            ListMoviesQuery query = new ListMoviesQuery();
            var movies = await _mediator.Send(query);

            return Ok(movies);
        }

        [HttpGet("savetopfivesearch")]
        public async Task<IActionResult> SaveTopFiveSearch(string title, int page)
        {
            var query = new
            {
                Title = title,
                Page = page
            };

            MovieSearchByTitleQuery command = new MovieSearchByTitleQuery(query.Title, query.Page);
            MovieSearchResultDto searchResult = await _mediator.Send(command);


            if (searchResult == null)
                return NotFound();

            var topFiveResult = searchResult.Search.Take(5).ToList();
            DateTime SearchDate = DateTime.UtcNow.AddHours(1);

            // Create a list of Movie objects from the top five search results
            var moviesToAdd = topFiveResult.Select(result => new Movie
            {
                Title = result.Title,
                Year = result.Year,
                ImdbId= result.imdbID,  
                PosterUrl=result.Poster,
                Type = result.Type,
                SearchDate = SearchDate,
            }).ToList();

            // Create the AddMoviesCommand with the list of movies
            var addMoviesCommand = new AddMoviesCommand(moviesToAdd);

            // Send the command to add movies to the database
            await _mediator.Send(addMoviesCommand);

            return Ok(searchResult);
        }
    }
}
