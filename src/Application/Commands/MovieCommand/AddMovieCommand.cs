using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MovieCommand
{ 
    public sealed class AddMovieCommand : IRequest
    {
        public AddMovieCommand(Movie movie)
        {
            Movie = movie;
        }

        public Movie Movie { get; set; }


    }

    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public AddMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(AddMovieCommand request, CancellationToken cancellationToken)
        { 
          
            await _movieRepository.AddAsync(request.Movie);


        }
    }
}
