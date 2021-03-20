using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_IS413_Assignment9.Models
{
    public class EFMoviesRepository : IMoviesRepository
    {
        private MoviesDBContext _context;

        //Constructor
        public EFMoviesRepository(MoviesDBContext context)
        {
            _context = context;
        }
        public IQueryable<Movies> Movies => _context.Movies;
    }
}
