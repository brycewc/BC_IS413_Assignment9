using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_IS413_Assignment9.Models
{
    public interface IMoviesRepository
    {
        IQueryable<Movies> Movies { get; }
    }
}
