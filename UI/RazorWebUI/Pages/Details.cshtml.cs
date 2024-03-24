using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebUI.Services;

namespace RazorWebUI.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMovieService _movieService;
        public DetailsModel(ILogger<IndexModel> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public MovieDetailsDto MovieDetailsDto { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {

                return RedirectToPage("./Movies");
            }
            MovieDetailsDto = await _movieService.GetMovieById(id);
            
            return Page();

        }
    }
}
