using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.ViewModels
{
    public class MoviesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Trivia { get; set; }
        public int FilmCount { get; set; }
        public int RentCount { get; set; }
        public bool Available { get; set; }
        public int StudioId { get; set; }
    }
}
