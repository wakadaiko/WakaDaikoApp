using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeCommentRepository : ICommentRepository
    {
        readonly List<Comment> _comments = [];

        public async Task<Comment> GetCommentByIdAsync(int id) => throw new NotImplementedException();

        public List<Comment> GetComments() => throw new NotImplementedException();

        public async Task<int> StoreCommentAsync(Comment comment)
        {
            comment.CommentId = _comments.Count + 1;

            _comments.Add(comment);

            return comment.CommentId;
        }

        public int UpdateComment(Comment comment) => throw new NotImplementedException();

        public int DeleteComment(int commentId) => throw new NotImplementedException();
    }
}
