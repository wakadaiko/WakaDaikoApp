using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface ITaikoVideosRepository
    {
        public interface IBlogTaikoVideosRepository
        {
            public List<TaikoVideos> GetTaikoVideoss();

            public Task<TaikoVideos> GetTaikoVideosByIdAsync(int id);

            public Task<int> StoreTaikoVideosAsync(TaikoVideos comment);

            public int UpdateTaikoVideos(TaikoVideos comment);

            public int DeleteTaikoVideos(int commentId);
        }
    }
}
