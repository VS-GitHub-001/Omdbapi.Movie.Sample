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
    
    public sealed class MovieGetByIdQuery : IRequest<MovieDetailsDto>
    {
        public MovieGetByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
 
        public class MovieGetByIdHandler : IRequestHandler<MovieGetByIdQuery, MovieDetailsDto>
        {
            private readonly IOmdbApiClient _omdbApiClient;

            public MovieGetByIdHandler(IOmdbApiClient omdbApiClient)
            {
                _omdbApiClient = omdbApiClient;
            }

            public async Task<MovieDetailsDto> Handle(MovieGetByIdQuery request, CancellationToken cancellationToken)
            {
                return await _omdbApiClient.GetMovieByIdAsync(request.Id);

            }
        }
    }

}
