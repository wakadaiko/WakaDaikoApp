using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface ITaikoAudioRepository
    {
        public interface IBlogTaikoAudioRepository
        {
            public List<TaikoAudio> GetTaikoAudios();

            public Task<TaikoAudio> GetTaikoAudioByIdAsync(int id);

            public Task<int> StoreTaikoAudioAsync(TaikoAudio comment);

            public int UpdateTaikoAudio(TaikoAudio comment);

            public int DeleteTaikoAudio(int commentId);
        }
    }
}
