using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeTaikoAudioRepository : ITaikoAudioRepository
    {
        readonly List<TaikoAudio> _taikoaudio = [];

        public async Task<TaikoAudio> GetTaikoAudioByIdAsync(int id) => throw new NotImplementedException();

        public List<TaikoAudio> GetTaikoAudios() => throw new NotImplementedException();

        public async Task<int> StoreTaikoAudioAsync(TaikoAudio comment)
        {
            comment.AudioId = _taikoaudio.Count + 1;

            _taikoaudio.Add(comment);

            return comment.AudioId;
        }

        public int UpdateTaikoAudio(TaikoAudio comment) => throw new NotImplementedException();

        public int DeleteTaikoAudio(int commentId) => throw new NotImplementedException();
    }
}
