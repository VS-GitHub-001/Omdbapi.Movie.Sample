using API.Controllers;
using Application.Queries.OmdbQueries;
using Domain.DTOs;
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
    public class OmdbQueryControllerTests
    {
        [Fact]
        public async Task SearchMoviesByTitle_Returns_Ok_With_SearchResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var expectedResult = new MovieSearchResultDto(); // Your expected search result here

            mediatorMock.Setup(m => m.Send(It.IsAny<MovieSearchByTitleQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedResult);

            var controller = new OmdbQueryController(mediatorMock.Object);

            // Act
            var result = await controller.SearchMoviesByTitle("YourTitle", 1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(expectedResult, okResult.Value);
        }



        [Fact]
        public async Task GetMovieByTitle_Returns_Ok_With_MovieResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var expectedResult = new MovieDetailsDto(); // Your expected movie details here

            mediatorMock.Setup(m => m.Send(It.IsAny<MovieGetByTitleQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedResult);

            var controller = new OmdbQueryController(mediatorMock.Object);

            // Act
            var result = await controller.GetMovieByTitle("YourTitle");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetMovieById_Returns_Ok_With_MovieResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var expectedResult = new MovieDetailsDto(); // Your expected movie details here

            mediatorMock.Setup(m => m.Send(It.IsAny<MovieGetByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(expectedResult);

            var controller = new OmdbQueryController(mediatorMock.Object);

            // Act
            var result = await controller.GetMovieById("YourId");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(expectedResult, okResult.Value);
        }
    }
}