using FilmStudio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmStudio.Data
{
    public class DbConnect : IdentityDbContext<Users>
    {
        public DbConnect(DbContextOptions<DbConnect> options) : base(options) { }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<Studios> Studios { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed filmstudios
            modelBuilder.Entity<Studios>().HasData(
                    new Studios { Id = 1, Name = "Studio01", Place = "Stockholm" },
                    new Studios { Id = 2, Name = "Studio02", Place = "Göteborg" },
                    new Studios { Id = 3, Name = "Studio03", Place = "Malmö" },
                    new Studios { Id = 4, Name = "Studio04", Place = "Växjö" }
           );

            // seed moview
            modelBuilder.Entity<Movies>().HasData(
                    new Movies(1, "Movie01", "Comedy, comedic, funny event"),
                    new Movies(2, "Movie02", "Action, horror, sensational film, action film, horror film"),
                    new Movies(3, "Movie03", "Comedy, comedic, funny event"),
                    new Movies(4, "Movie04", "Thriller, action, horror, sensational film, action film, horror film")
           );
        }
    }
}
