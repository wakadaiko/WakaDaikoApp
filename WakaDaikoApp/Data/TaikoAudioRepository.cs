using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class TaikoAudioRepository(AppDbContext c) : ITaikoAudioRepository
    {
        readonly AppDbContext _context = c;

        public async Task<TaikoAudio> GetTaikoAudioByIdAsync(int id)
        {
            var comment = await _context.TaikoAudio.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<TaikoAudio> GetTaikoAudio()
        {
            return [.. _context.TaikoAudio.Include(m => m.AudioId)];
        }

        public async Task<int> StoreTaikoAudioAsync(TaikoAudio comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateTaikoAudio(TaikoAudio comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteTaikoAudio(int commentId)
        {
            TaikoAudio comment = GetTaikoAudioByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.TaikoAudio.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
