using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IOmdbApiClient
    {
        Task<MovieSearchResultDto> SearchByTitleAsync(string title, int? page = null);

        Task<MovieDetailsDto> GetMovieByIdAsync(string imdbId);

        Task<MovieDetailsDto> GetMovieByTitleAsync(string title);

    }
}
