using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeTaikoTextRepository : ITaikoTextRepository
    {
        readonly List<TaikoText> _taikotext = [];

        public async Task<TaikoText> GetTaikoTextByIdAsync(int id) => throw new NotImplementedException();

        public List<TaikoText> GetTaikoTexts() => throw new NotImplementedException();

        public async Task<int> StoreTaikoTextAsync(TaikoText comment)
        {
            comment.TextId = _taikotext.Count + 1;

            _taikotext.Add(comment);

            return comment.TextId;
        }

        public int UpdateTaikoText(TaikoText comment) => throw new NotImplementedException();

        public int DeleteTaikoText(int commentId) => throw new NotImplementedException();
    }
}
