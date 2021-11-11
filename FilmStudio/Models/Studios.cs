using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Models
{
    public class Studios
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }

        public List<Ratings> Ratings { get; set; }
    }
}
