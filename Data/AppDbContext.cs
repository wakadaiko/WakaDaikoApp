using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WakaDaikoApp.Models;
namespace WakaDaikoApp.Data 
{
    public class AppDbContext : IdentityDbContext{
        public AppDbContext(
           DbContextOptions<AppDbContext> options) : base(options) { }

        // one DbSet for each domain model class
        DbSet<Team> Teams { get; set; }

    }
}