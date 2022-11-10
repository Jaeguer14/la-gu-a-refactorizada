#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppMovie.Models;

    public class AppMovieContext : DbContext
    {
        public AppMovieContext (DbContextOptions<AppMovieContext> options)
            : base(options)
        {
        }

        public DbSet<AppMovie.Models.Section> Section { get; set; }
        public DbSet<AppMovie.Models.Producer> Producer { get; set; }
        public DbSet<AppMovie.Models.Location> Location { get; set; }
        public DbSet<AppMovie.Models.Partner> Partner { get; set; }
        public DbSet<AppMovie.Models.Gender> Gender { get; set; }
        public DbSet<AppMovie.Models.Movie> Movie { get; set; }
        public DbSet<AppMovie.Models.Country> Country { get; set; }
        public DbSet<AppMovie.Models.Rental> Rental { get; set; }
        public DbSet<AppMovie.Models.RentalDetail> RentalDetail { get; set; }
        public DbSet<AppMovie.Models.RentalDetailTemp> RentalDetailTemp { get; set; }
        public DbSet<AppMovie.Models.Return> Return { get; set; }

         public DbSet<AppMovie.Models.ReturnDetail> ReturnDetail { get; set; }
        public DbSet<AppMovie.Models.ReturnDetailTemp> ReturnDetailTemp { get; set; }


    }