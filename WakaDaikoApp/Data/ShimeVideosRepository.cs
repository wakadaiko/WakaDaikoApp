using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class ShimeVideosRepository(AppDbContext c) : IShimeVideosRepository
    {
        readonly AppDbContext _context = c;

        public async Task<ShimeVideos> GetShimeVideosByIdAsync(int id)
        {
            var comment = await _context.ShimeVideos.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<ShimeVideos> GetShimeVideos()
        {
            return [.. _context.ShimeVideos.Include(m => m.VideoId)];
        }

        public async Task<int> StoreShimeVideosAsync(ShimeVideos comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateShimeVideos(ShimeVideos comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteShimeVideos(int commentId)
        {
            ShimeVideos comment = GetShimeVideosByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.ShimeVideos.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
