using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}
