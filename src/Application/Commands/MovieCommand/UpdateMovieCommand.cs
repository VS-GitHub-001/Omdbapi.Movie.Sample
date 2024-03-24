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
 
    public sealed class UpdateMovieCommand : IRequest
    {
        public UpdateMovieCommand(Movie movie)
        {
            Movie = movie;
        }

        public Movie Movie { get; set; }


    }

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {

            await _movieRepository.UpdateAsync(request.Movie);
        }
    }
}
