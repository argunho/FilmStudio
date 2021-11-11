using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int FilmCount { get; set; }
        public bool Available { get; set; }
        public int RentCount { get; set; }
        public string Trivia { get; set; }

        public List<Studios> Studios { get; set; }
        public List<Ratings> Ratings { get; set; }

        public Movies() { }
        public Movies(int id, string title, string text, int count = 5) {
            Id = id;
            Title = title;
            Text = text;
            FilmCount = count;
            Available = true;
        }
    }
}
