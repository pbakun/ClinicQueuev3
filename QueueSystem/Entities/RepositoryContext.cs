using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext
    {
        //private readonly IConfiguration _configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AppData/AppData.db3");
            //optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }


        public DbSet<Models.Queue> Queue { get; set; }
        public DbSet<User> User { get; set; }
    }
}
