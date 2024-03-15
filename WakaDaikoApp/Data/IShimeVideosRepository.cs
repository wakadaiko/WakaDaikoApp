using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IShimeVideosRepository
    {
        public interface IBlogShimeVideosRepository
        {
            public List<ShimeVideos> GetShimeVideoss();

            public Task<ShimeVideos> GetShimeVideosByIdAsync(int id);

            public Task<int> StoreShimeVideosAsync(ShimeVideos comment);

            public int UpdateShimeVideos(ShimeVideos comment);

            public int DeleteShimeVideos(int commentId);
        }
    }
}
