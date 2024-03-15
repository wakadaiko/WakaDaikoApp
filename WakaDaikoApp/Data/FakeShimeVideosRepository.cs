using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeShimeVideosRepository : IShimeVideosRepository
    {
        readonly List<ShimeVideos> _shimevideos = [];

        public async Task<ShimeVideos> GetShimeVideosByIdAsync(int id) => throw new NotImplementedException();

        public List<ShimeVideos> GetShimeVideoss() => throw new NotImplementedException();

        public async Task<int> StoreShimeVideosAsync(ShimeVideos comment)
        {
            comment.VideoId = _shimevideos.Count + 1;

            _shimevideos.Add(comment);

            return comment.VideoId;
        }

        public int UpdateShimeVideos(ShimeVideos comment) => throw new NotImplementedException();

        public int DeleteShimeVideos(int commentId) => throw new NotImplementedException();
    }
}
