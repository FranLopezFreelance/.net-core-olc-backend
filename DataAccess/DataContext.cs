using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options): base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CourseInstructor>().HasKey(k => new { k.InstructorId, k.CourseId });
        }

        public DbSet<Course> Course { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<CourseInstructor> CourseInstructor { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
}
