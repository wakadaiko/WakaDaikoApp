using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class TaikoTextRepository(AppDbContext c) : ITaikoTextRepository
    {
        readonly AppDbContext _context = c;

        public async Task<TaikoText> GetTaikoTextByIdAsync(int id)
        {
            var comment = await _context.TaikoText.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<TaikoText> GetTaikoText()
        {
            return [.. _context.TaikoText.Include(m => m.TextId)];
        }

        public async Task<int> StoreTaikoTextAsync(TaikoText comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateTaikoText(TaikoText comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteTaikoText(int commentId)
        {
            TaikoText comment = GetTaikoTextByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.TaikoText.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
