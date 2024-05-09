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
        public DbSet<AudioResource> AudioResources { get; set; }
        public DbSet<VideoResource> VideoResources { get; set; }
        public DbSet<TextResource> TextResources { get; set; }

    }
}
