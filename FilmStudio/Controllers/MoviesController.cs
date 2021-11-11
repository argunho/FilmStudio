using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmStudio.Data;
using FilmStudio.Models;
using FilmStudio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DbConnect _db;

        public MoviesController(DbConnect db)
        {
            _db = db;
        }

        private IEnumerable<Movies> AllMovies
        {
            get
            {
                return _db.Movies.Include(s => s.Studios).Include(r => r.Ratings).ToList();
            }
        }

        #region GET
        // GET: All movies. api/Movies
        [HttpGet]
        public IEnumerable<Movies> Get()
        {
            return AllMovies;
        }

        // GET: Movie by id: api/Movies/5
        [HttpGet("{id}")]
        public Movies GetMovies(int id)
        {
            return AllMovies.FirstOrDefault(x => x.Id == id);
        }

        // GET: Rented out movies. api/Movies/rentedout
        [HttpGet("rentedout")]
        public IEnumerable<Movies> GetRentedOut()
        {
            return AllMovies.Where(x => x.RentCount > 0).ToList();
        }

        // GET: Movies, available to rent. api/Movies/available
        [HttpGet("available")]
        public IEnumerable<Movies> GetRetedOut()
        {
            return AllMovies.Where(x => x.Available && x.FilmCount > 0).ToList();
        }

        //GET: Get studios by film id. api/Movies/GetStudiosByFilm/1
        [HttpGet("GetStudiosByFilm/{id}")]
        public List<Studios> GetStudiosByFilm(int id)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            return movie.Studios.ToList();
        }
        #endregion

        #region POST
        // POST: Add new movie. api/Movies/NewMovies
        [HttpPost("NewFilm")]
        public JsonResult NewFilm(MoviesViewModel model)
        {
            if (model == null)
                return new JsonResult(new { error = true, msg = "Ingen data har skickats ..." });

            var movie = new Movies
            {
                Title = model.Title,
                Text = model.Text,
                Trivia = model.Trivia,
                FilmCount = model.FilmCount,
                Available = model.Available
            };
            _db.Movies.Add(movie);
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }
        #endregion

        #region PUT
        //PUT: Change Movies status available or not to rent out. api/Movies/Availablle/5
        [HttpPut("available/{id}/{param}")]
        public JsonResult ChangeAvailableStatus(int id, bool param)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return new JsonResult(new { error = true, msg = "Film kunde inte hittats ..." });

            movie.Available = param;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }

        //PUT:  Rent a Movie to studio. api/Movies/1/1
        [HttpPut("{id}/{studio}")]
        public JsonResult RentMovie(int id, int studio)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            var filmstudio = _db.Studios.FirstOrDefault(x => x.Id == studio);
            if (movie == null || filmstudio == null)
                return new JsonResult(new { error = true, msg = "Film eller studio kunde inte hittats ..." }); 
            if(movie.RentCount == 0 || !movie.Available)
                return new JsonResult(new { error = true, msg = "Film får inte hyras ut ..." });
            if(movie.Studios.FirstOrDefault(x => x.Id == filmstudio.Id) != null)
                return new JsonResult(new { error = true, msg = "Film är redan uthyrd av denna studio ..." });

            movie.Studios.Add(filmstudio);
            movie.RentCount += 1;
            movie.FilmCount -= 1;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }

        //PUT: Return a Movie from studio. api/Movies/return/1/1
        [HttpPut("return/{id}/{studio}")]
        public JsonResult ReturnMovies(int id, int studio)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            var filmstudio = _db.Studios.FirstOrDefault(x => x.Id == studio);
            if (movie == null || filmstudio == null
                            || movie.Studios.FirstOrDefault(x => x.Id == filmstudio.Id) == null)
                return new JsonResult(new { error = true, msg = "Film eller studio kunde inte hittats ..." });

            movie.Studios.Remove(filmstudio);
            movie.RentCount -= 1;
            movie.FilmCount += 1;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }

        //PUT: Change count of movies
        [HttpPut("count/{id}/{count}")]
        public JsonResult ChangeCountOfMovie(int id, int count)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return new JsonResult(new { error = true, msg = "Film kunde inte hittats ..." });

            movie.FilmCount = count;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }

        //PUT: Set or update movie's trivia
        [HttpPut("trivia/{id}")]
        public JsonResult SetUpdateTrivia(int id, MoviesViewModel model) {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return new JsonResult(new { error = true, msg = "Film kunde inte hittats ..." });

            movie.Trivia = model.Trivia;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }        
        
        [HttpPut("removeTrivia/{id}")]
        public JsonResult RemoveTrivia(int id)
        {
            var movie = AllMovies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return new JsonResult(new { error = true, msg = "Film kunde inte hittats ..." });

            movie.Trivia = null;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }       

        #endregion

        #region Helpers 
        // Help method
        private new string Response()
        {
            try { _db.SaveChanges(); }
            catch (Exception e) { return e.Message; }

            return String.Empty;
        }
        #endregion

    }
}
