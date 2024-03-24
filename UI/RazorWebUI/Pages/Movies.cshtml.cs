using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebUI.Services;

namespace RazorWebUI.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMovieService _movieService;
        public MoviesModel(ILogger<IndexModel> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }
         public Dictionary<DateTime, List<Movie>> MoviesGroupedByDate { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var movies = await _movieService.GetMovies();
            // Group the movies by their release date
            MoviesGroupedByDate = movies.GroupBy(movie => movie.SearchDate)
                                        .ToDictionary(group => group.Key, group => group.ToList());

            return Page();

        }
    }
}
