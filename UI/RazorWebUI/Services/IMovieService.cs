using Domain.DTOs;
using Domain.Models;

namespace RazorWebUI.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
        Task<List<Movie>> SaveTopFiveSearch(string title, int page);
        Task<MovieDetailsDto> GetMovieById(string id);
    }
}
