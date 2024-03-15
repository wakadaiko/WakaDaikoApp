using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeShimeAudioRepository : IShimeAudioRepository
    {
        readonly List<ShimeAudio> _shimeaudio = [];

        public async Task<ShimeAudio> GetShimeAudioByIdAsync(int id) => throw new NotImplementedException();

        public List<ShimeAudio> GetShimeAudios() => throw new NotImplementedException();

        public async Task<int> StoreShimeAudioAsync(ShimeAudio comment)
        {
            comment.AudioId = _shimeaudio.Count + 1;

            _shimeaudio.Add(comment);

            return comment.AudioId;
        }

        public int UpdateShimeAudio(ShimeAudio comment) => throw new NotImplementedException();

        public int DeleteShimeAudio(int commentId) => throw new NotImplementedException();
    }
}
