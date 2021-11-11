using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmStudio.Data;
using FilmStudio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly DbConnect _db;

        public RatingsController(DbConnect db)
        {
            _db = db;
        }

        private IEnumerable<Ratings> AllRatings
        {
            get
            {
                return _db.Ratings.Include(s => s.Studio).Include(m => m.Movie).ToList();
            }
        }

        #region POST
        [HttpPost("NewRating")]
        public JsonResult NewRating(Ratings model) {
            if (model == null)
                return new JsonResult(new { error = true, msg = "Ingen data var skickad ..." });

            var movie = _db.Movies.FirstOrDefault(x => x.Id == model.MovieId);
            if (movie == null)
                return new JsonResult(new { error = true, msg = "Film har inte hittats ..." });

            var studio = _db.Studios.FirstOrDefault(x => x.Id == model.StudioId);
            if (studio == null)
                return new JsonResult(new { error = true, msg = "Studio har inte hittats ..." });

            var rating = new Ratings
            {
                Rating = model.Rating,
                MovieId = movie.Id,
                Movie = movie,
                StudioId = studio.Id,
                Studio  = studio
            };
            _db.Ratings.Add(rating);
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
