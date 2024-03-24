using Domain.DTOs;
using Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.OmdbQueries
{



    public sealed class MovieSearchByTitleQuery : IRequest<MovieSearchResultDto>
    {
        public MovieSearchByTitleQuery(string title, int page)
        {
            Title = title;
            Page = page;
        }

        public string Title { get; set; }
        public int Page { get; set; }

        public class MovieSearchByTitleHandler : IRequestHandler<MovieSearchByTitleQuery, MovieSearchResultDto>
        {
            private readonly IOmdbApiClient _omdbApiClient;

            public MovieSearchByTitleHandler(IOmdbApiClient omdbApiClient)
            {
                _omdbApiClient = omdbApiClient;
            }

            public async Task<MovieSearchResultDto> Handle(MovieSearchByTitleQuery request, CancellationToken cancellationToken)
            {
                return await _omdbApiClient.SearchByTitleAsync(request.Title, request.Page);

            }
        }
    }



}
