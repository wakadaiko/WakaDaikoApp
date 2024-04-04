using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class TaikoVideosRepository(AppDbContext c) : ITaikoVideosRepository
    {
        readonly AppDbContext _context = c;

        public async Task<TaikoVideos> GetTaikoVideosByIdAsync(int id)
        {
            var comment = await _context.TaikoVideos.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<TaikoVideos> GetTaikoVideos()
        {
            return [.. _context.TaikoVideos.Include(m => m.VideoId)];
        }

        public async Task<int> StoreTaikoVideosAsync(TaikoVideos comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateTaikoVideos(TaikoVideos comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteTaikoVideos(int commentId)
        {
            TaikoVideos comment = GetTaikoVideosByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.TaikoVideos.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
