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
   
    public sealed class MovieGetByTitleQuery : IRequest<MovieDetailsDto>
    {
        public MovieGetByTitleQuery(string title)
        {
            Title = title; 
        }

        public string Title { get; set; }
        public int Page { get; set; }

        public class MovieGetByTitleHandler : IRequestHandler<MovieGetByTitleQuery, MovieDetailsDto>
        {
            private readonly IOmdbApiClient _omdbApiClient;

            public MovieGetByTitleHandler(IOmdbApiClient omdbApiClient)
            {
                _omdbApiClient = omdbApiClient;
            }

            public async Task<MovieDetailsDto> Handle(MovieGetByTitleQuery request, CancellationToken cancellationToken)
            {
                return await _omdbApiClient.GetMovieByTitleAsync(request.Title);

            }
        }
    }
}
