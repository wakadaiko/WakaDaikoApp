using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class ShimeAudioRepository(AppDbContext c) : IShimeAudioRepository
    {
        readonly AppDbContext _context = c;

        public async Task<ShimeAudio> GetShimeAudioByIdAsync(int id)
        {
            var comment = await _context.ShimeAudio.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<ShimeAudio> GetShimeAudio()
        {
            return [.. _context.ShimeAudio.Include(m => m.AudioId)];
        }

        public async Task<int> StoreShimeAudioAsync(ShimeAudio comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateShimeAudio(ShimeAudio comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteShimeAudio(int commentId)
        {
            ShimeAudio comment = GetShimeAudioByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.ShimeAudio.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
