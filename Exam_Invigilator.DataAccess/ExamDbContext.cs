using Exam_Invigilator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exam_Invigilator.DataAccess
{

    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) { }

        public DbSet<Invigilator> Invigilators { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123" // for development only
                });

            // Venue seed
            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = 1, Name = "Block A", HallNumber = "H1" },
                new Venue { Id = 4, Name = "Block B", HallNumber = "H2" },
                new Venue { Id = 2, Name = "Block C", HallNumber = "H3" },
                new Venue { Id = 3, Name = "Block D", HallNumber = "H4" }
            );

        }



    }
}
