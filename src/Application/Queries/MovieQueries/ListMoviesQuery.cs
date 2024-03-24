using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MovieQueries
{
   

    public sealed class ListMoviesQuery : IRequest<List<Movie>>
    {
        public class ListMoviesQueryHandler : IRequestHandler<ListMoviesQuery, List<Movie>>
        {
            private readonly IMovieRepository _movieRepository;

            public ListMoviesQueryHandler(IMovieRepository movieRepository)
            {
                _movieRepository = movieRepository;
            }

            public async Task<List<Movie>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
            {
                return await _movieRepository.GetAllAsync();

            }
        }
    }

}
