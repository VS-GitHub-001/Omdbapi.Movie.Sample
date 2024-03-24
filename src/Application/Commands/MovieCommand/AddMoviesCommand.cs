using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MovieCommand
{
    public sealed class AddMoviesCommand : IRequest
    {
        public AddMoviesCommand(IEnumerable<Movie> movies)
        {
            Movies = movies.ToList();
        }

        public List<Movie> Movies { get; set; }
    }

    public class AddMoviesCommandHandler : IRequestHandler<AddMoviesCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public AddMoviesCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(AddMoviesCommand request, CancellationToken cancellationToken)
        {
            foreach (var movie in request.Movies)
            {
                await _movieRepository.AddAsync(movie);
            }
        }
    }

}
