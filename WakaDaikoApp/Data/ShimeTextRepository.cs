using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class ShimeTextRepository(AppDbContext c) : IShimeTextRepository
    {
        readonly AppDbContext _context = c;

        public async Task<ShimeText> GetShimeTextByIdAsync(int id)
        {
            var comment = await _context.ShimeText.FindAsync(id);

            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<ShimeText> GetShimeText()
        {
            return [.. _context.ShimeText.Include(m => m.TextId)];
        }

        public async Task<int> StoreShimeTextAsync(ShimeText comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateShimeText(ShimeText comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteShimeText(int commentId)
        {
            ShimeText comment = GetShimeTextByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.ShimeText.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
