using Domain.GenericInterface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{

    public interface IMovieRepository : IRepository<Movie>
    {
        // Define additional methods specific to movies if needed
        Task<Movie> GetByImdbIdAsync(string imdbId);
    }

}
