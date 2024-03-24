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

    public sealed class DeleteMovieCommand : IRequest
    {
        public DeleteMovieCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
         
    }

    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {

            var movie = await _movieRepository.GetByIdAsync(request.Id);

            await _movieRepository.RemoveAsync(movie);

        }
    }
}
