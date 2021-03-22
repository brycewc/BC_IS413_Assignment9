using BC_IS413_Assignment9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BC_IS413_Assignment9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MoviesDBContext _context;

        private IMoviesRepository _repository;

        public static int staticID; //global variable to store id of editing movie between pages

        //Constructor
        public HomeController(ILogger<HomeController> logger, MoviesDBContext context, IMoviesRepository repository)
        {
            _logger = logger;
            _context = context;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcast()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovies(Movies movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return View("ViewMovies", _context.Movies);
            }
            return View("AddMovies");
        }

        [HttpPost]
        public IActionResult EditMovies(int id)
        {
            staticID = id;
            return View("EditMovies", new MoviesViewModel {
                MoviesModel = _context.Movies.Single(x => x.MovieID == staticID),
                ID = staticID
            });
        }

        [HttpPost]
        public IActionResult UpdateMovies(MoviesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _context.Movies.Single(x => x.MovieID == staticID);
                //Can't use shorter method to do in one line, because ID is not being changed
                _context.Entry(movie).Property(x => x.Category).CurrentValue = model.MoviesModel.Category;
                _context.Entry(movie).Property(x => x.Title).CurrentValue = model.MoviesModel.Title;
                _context.Entry(movie).Property(x => x.Year).CurrentValue = model.MoviesModel.Year;
                _context.Entry(movie).Property(x => x.Director).CurrentValue = model.MoviesModel.Director;
                _context.Entry(movie).Property(x => x.Rating).CurrentValue = model.MoviesModel.Rating;
                _context.Entry(movie).Property(x => x.Edited).CurrentValue = model.MoviesModel.Edited;
                _context.Entry(movie).Property(x => x.LentTo).CurrentValue = model.MoviesModel.LentTo;
                _context.Entry(movie).Property(x => x.Notes).CurrentValue = model.MoviesModel.Notes;
                _context.SaveChanges();
                return RedirectToAction("ViewMovies");
            }
            else
            {
                return View(new MoviesViewModel
                {
                    MoviesModel = _context.Movies.Single(x => x.MovieID == staticID), //single allows casting an IQueryable query that will end in one result becoming a model
                    ID = staticID
                });
            }
        }

        public IActionResult DeleteMovies(int id)
        {
            _context.Remove(_context.Movies.Single(x => x.MovieID == id));
            _context.SaveChanges();
            return RedirectToAction("ViewMovies");
        }

        public IActionResult ViewMovies()
        {
            return View(_context.Movies);
        }

        //From default project creation
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
