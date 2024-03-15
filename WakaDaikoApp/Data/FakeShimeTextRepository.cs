using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeShimeTextRepository : IShimeTextRepository
    {
        readonly List<ShimeText> _shimetext = [];

        public async Task<ShimeText> GetShimeTextByIdAsync(int id) => throw new NotImplementedException();

        public List<ShimeText> GetShimeTexts() => throw new NotImplementedException();

        public async Task<int> StoreShimeTextAsync(ShimeText comment)
        {
            comment.TextId = _shimetext.Count + 1;

            _shimetext.Add(comment);

            return comment.TextId;
        }

        public int UpdateShimeText(ShimeText comment) => throw new NotImplementedException();

        public int DeleteShimeText(int commentId) => throw new NotImplementedException();
    }
}
