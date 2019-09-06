using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AppData.db3");
        }

        public DbSet<Models.Queue> Queue { get; set; }
        public DbSet<User> User { get; set; }
    }
}
