using Application.Queries.OmdbQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OmdbQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OmdbQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("searchmoviesbytitle")]
        public async Task<IActionResult> SearchMoviesByTitle(string title, int page)
        {
            var query = new
            {
                Title = title,
                Page = page
            };

             MovieSearchByTitleQuery command = new MovieSearchByTitleQuery(query.Title, query.Page);
            var searchResult = await _mediator.Send(command);


            if (searchResult == null)
                return NotFound();

            return Ok(searchResult);
        }

        [HttpGet("getmoviebytitle")]
        public async Task<IActionResult> GetMovieByTitle(string title)
        {
            var query = new
            {
                Title = title
            };

             MovieGetByTitleQuery command = new MovieGetByTitleQuery(query.Title);
            var movieResult = await _mediator.Send(command);


            if (movieResult == null)
                return NotFound();

            return Ok(movieResult);
        }

        [HttpGet("getmoviebyid/{id}")]
        public async Task<IActionResult> GetMovieById(string id)
        {
            var query = new
            {
                Id = id
            };

             MovieGetByIdQuery command = new MovieGetByIdQuery(query.Id);
            var movieResult = await _mediator.Send(command);


            if (movieResult == null)
                return NotFound();

            return Ok(movieResult);
        }
    }
}
