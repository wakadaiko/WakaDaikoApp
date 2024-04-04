using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface ICommentRepository
    {
        public interface IBlogCommentRepository
        {
            public List<Comment> GetComments();

            public Task<Comment> GetCommentByIdAsync(int id);

            public Task<int> StoreCommentAsync(Comment comment);

            public int UpdateComment(Comment comment);

            public int DeleteComment(int commentId);
        }
    }
}
