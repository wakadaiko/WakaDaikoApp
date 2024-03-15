using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeTaikoVideosRepository : ITaikoVideosRepository
    {
        readonly List<TaikoVideos> _taikovideos = [];

        public async Task<TaikoVideos> GetTaikoVideosByIdAsync(int id) => throw new NotImplementedException();

        public List<TaikoVideos> GetTaikoVideoss() => throw new NotImplementedException();

        public async Task<int> StoreTaikoVideosAsync(TaikoVideos comment)
        {
            comment.VideoId = _taikovideos.Count + 1;

            _taikovideos.Add(comment);

            return comment.VideoId;
        }

        public int UpdateTaikoVideos(TaikoVideos comment) => throw new NotImplementedException();

        public int DeleteTaikoVideos(int commentId) => throw new NotImplementedException();
    }
}
