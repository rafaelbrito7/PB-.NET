using Microsoft.EntityFrameworkCore;
using Entities;
using System;

namespace Repository
{
    public class PBContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UsersFollowers> UsersFollowers { get; set; }

        public PBContext(DbContextOptions<PBContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}