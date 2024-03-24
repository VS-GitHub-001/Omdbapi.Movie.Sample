using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context) : base(context)
        {
            _context = context;
        }

        // Implement additional methods specific to movies
        public async Task<Movie> GetByImdbIdAsync(string imdbId)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.ImdbId == imdbId);
        }
    }
}
