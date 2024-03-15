using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<ShimeVideos> ShimeVideos { get; set; }

        public DbSet<ShimeText> ShimeText { get; set; }

        public DbSet<ShimeAudio> ShimeAudio { get; set; }

        public DbSet<TaikoVideos> TaikoVideos { get; set; }

        public DbSet<TaikoText> TaikoText { get; set; }

        public DbSet<TaikoAudio> TaikoAudio { get; set; }
    }
}
