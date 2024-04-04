using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class CommentRepository(AppDbContext c) : ICommentRepository
    {
        readonly AppDbContext _context = c;

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            _context.Entry(comment).Reference(m => m.Author).Load();
            _context.Entry(comment).Collection(m => m.Comments).Load();

            return comment;
        }

        public List<Comment> GetComments()
        {
            return [.. _context.Comments.Include(m => m.Author)];
        }

        public async Task<int> StoreCommentAsync(Comment comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateComment(Comment comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteComment(int commentId)
        {
            Comment comment = GetCommentByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.Comments.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
