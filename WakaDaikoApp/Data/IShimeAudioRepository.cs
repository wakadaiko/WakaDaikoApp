using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IShimeAudioRepository
    {
        public interface IBlogShimeAudioRepository
        {
            public List<ShimeAudio> GetShimeAudios();

            public Task<ShimeAudio> GetShimeAudioByIdAsync(int id);

            public Task<int> StoreShimeAudioAsync(ShimeAudio comment);

            public int UpdateShimeAudio(ShimeAudio comment);

            public int DeleteShimeAudio(int commentId);
        }
    }
}
