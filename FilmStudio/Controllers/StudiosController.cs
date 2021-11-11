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
    public class StudiosController : ControllerBase
    {
        private readonly DbConnect _db;

        public StudiosController(DbConnect db)
        {
            _db = db;
        }

        private IEnumerable<Studios> AllStudios
        {
            get
            {
                return _db.Studios.Include(r => r.Ratings).ToList();
            }
        }

        #region GET
        //GET: All studios
        [HttpGet]
        public IEnumerable<Studios> Get()
        {
            return AllStudios;
        }

        //GET: Studio by id
        [HttpGet("{id}")]
        public Studios GetStudio(int id)
        {
            return AllStudios.FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region POST
        //POST: Ad new studio
        [HttpPost("NewStudio")]
        public JsonResult NewStudio(StudiosViewModel model) {
            if (model == null)
                return new JsonResult(new { error = true, msg = "Ingen data var skickad ..." });

            var studio = new Studios
            {
                Name = model.Name,
                Place = model.Place
            };
            _db.Studios.Add(studio);
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }
        #endregion

        #region PUT
        //PUT: Edit studio by id
        [HttpPut("edit/{id}")]
        public JsonResult EditStudio(int id, StudiosViewModel model) {
            var studio = AllStudios.FirstOrDefault(x => x.Id == id);
            if (studio == null)
                return new JsonResult(new { error = true, msg = "Studio kunde inte hittats ..." });
            if (model == null)
                return new JsonResult(new { error = true, msg = "Ingen data var skickad ..." });

            studio.Name = model.Name;
            studio.Place = model.Place;
            return new JsonResult(new { success = Response().Length == 0, message = Response() });
        }
        #endregion

        #region DELETE
        [HttpDelete("delete/{id}")]
        public JsonResult DeleteStudio(int id) {
            var studio = AllStudios.FirstOrDefault(x => x.Id == id);
            if (studio == null)
                return new JsonResult(new { error = true, msg = "Studio kunde inte hittats ..." });
            _db.Studios.Remove(studio);
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
