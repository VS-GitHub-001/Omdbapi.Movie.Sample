using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebUI.Services;

namespace RazorWebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMovieService _movieService;
        public IndexModel(ILogger<IndexModel> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public void OnGet()
        {

        }
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public int Page { get; set; } = 1;

        public List<Movie> Movies { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Movies = await _movieService.SaveTopFiveSearch(Title, Page);
            if(Movies == null)
            {
                StatusMessage = "Unable to fetch Data. Kindly Check the search word and try again";
                return Page();
            }
            return Page();

        }
    }
}
