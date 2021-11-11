using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Models
{
    public class Ratings
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public Users User { get; set; }
        public int StudioId { get; set; }
        public Studios Studio { get; set; }
        public int MovieId { get; set; }
        public Movies Movie { get; set; }
    }
}
