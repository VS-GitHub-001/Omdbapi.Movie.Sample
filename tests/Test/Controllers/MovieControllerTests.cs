using API.Controllers;
using Application.Commands.MovieCommand;
using Application.Queries.MovieQueries;
using Application.Queries.OmdbQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class MovieControllerTests
    {
        private readonly MovieController _controller;
        private readonly Mock<IMediator> _mediatorMock;

        public MovieControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new MovieController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddMovie_ValidInput_ReturnsOk()
        {
            // Arrange
            var movie = new Movie {

                Title = "Jet Lag",
                Year = "2002",
                ImdbId = "tt0293116",
                Type = "movie",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTcyODgyMjU2NF5BMl5BanBnXkFtZTcwNDY3MDAwMQ@@._V1_SX300.jpg"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddMovieCommand>(), default(CancellationToken)))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddMovie(movie);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Test for UpdateMovie action
        [Fact]
        public async Task UpdateMovie_ValidInput_ReturnsOk()
        {
            // Arrange
            var movieId = 1; // Example movie ID
            var movie = new Movie { 
                Id = movieId,
                Title = "Jet Lag",
                Year = "2002",
                ImdbId = "tt0293116",
                Type = "movie",
                PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTcyODgyMjU2NF5BMl5BanBnXkFtZTcwNDY3MDAwMQ@@._V1_SX300.jpg"
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateMovieCommand>(), default(CancellationToken)))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateMovie(movieId, movie);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Test for DeleteMovie action
        [Fact]
        public async Task DeleteMovie_ValidInput_ReturnsOk()
        {
            // Arrange
            var movieId = "1"; // Example movie ID

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteMovieCommand>(), default(CancellationToken)))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteMovie(movieId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Example of testing ListMovies method
        [Fact]
        public async Task ListMovies_ReturnsListOfMovies()
        {
            // Arrange
            var movies = new List<Movie>
{
    new Movie
    {
        Title = "Pepsi, Where's My Jet?",
        Year = "2022",
        ImdbId = "tt23022276",
        Type = "series",
        PosterUrl = "https://m.media-amazon.com/images/M/MV5BNThmOTE2MWQtNWZmYi00MWExLThiNjktODM3NDFhNTNjOGM3XkEyXkFqcGdeQXVyNDY0NjE3NTY@._V1_SX300.jpg"
    },
    new Movie
    {
        Title = "Jet Lag",
        Year = "2002",
        ImdbId = "tt0293116",
        Type = "movie",
        PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTcyODgyMjU2NF5BMl5BanBnXkFtZTcwNDY3MDAwMQ@@._V1_SX300.jpg"
    },
 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<ListMoviesQuery>(), default(CancellationToken)))
                .ReturnsAsync(movies);

            // Act
            var result = await _controller.ListMovies();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<Movie>>(okResult.Value);
            Assert.Equal(movies.Count, model.Count);
        }


        [Fact]
        public async Task SaveTopFiveSearch_ReturnsOkResult()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var controller = new MovieController(mockMediator.Object);
            var searchResult = new MovieSearchResultDto
            {
                Search = new Search[]
                           {
                    new Search
                    {
                        Title = "Test Movie 1",
                        Year = "2020",
                        imdbID = "tt1234567",
                        Type = "movie",
                        Poster = "https://example.com/poster1.jpg"
                    },
                    new Search
                    {
                        Title = "Test Movie 2",
                        Year = "2021",
                        imdbID = "tt2345678",
                        Type = "movie",
                        Poster = "https://example.com/poster2.jpg"
                    },
                    new Search
                    {
                        Title = "Test Movie 3",
                        Year = "2022",
                        imdbID = "tt3456789",
                        Type = "movie",
                        Poster = "https://example.com/poster3.jpg"
                    },
                    new Search
                    {
                        Title = "Test Movie 4",
                        Year = "2023",
                        imdbID = "tt4567890",
                        Type = "movie",
                        Poster = "https://example.com/poster4.jpg"
                    },
                    new Search
                    {
                        Title = "Test Movie 5",
                        Year = "2024",
                        imdbID = "tt5678901",
                        Type = "movie",
                        Poster = "https://example.com/poster5.jpg"
                    },
                     new Search
                    {
                        Title = "Test Movie 6",
                        Year = "2023",
                        imdbID = "tt45678930",
                        Type = "movie",
                        Poster = "https://example.com/poster6.jpg"
                    },
                    new Search
                    {
                        Title = "Test Movie 7",
                        Year = "2024",
                        imdbID = "tt5678905",
                        Type = "movie",
                        Poster = "https://example.com/poster7.jpg"
                    }
                           }
            };
            
            mockMediator.Setup(m => m.Send(It.IsAny<MovieSearchByTitleQuery>(), CancellationToken.None))
                        .ReturnsAsync(searchResult);

            // Act
            var result = await controller.SaveTopFiveSearch("Test Title", 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(searchResult, result.Value);
            mockMediator.Verify(m => m.Send(It.IsAny<AddMoviesCommand>(), CancellationToken.None), Times.Once);
        }
    }
}
