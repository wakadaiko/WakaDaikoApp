using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface ITaikoTextRepository
    {
        public interface IBlogTaikoTextRepository
        {
            public List<TaikoText> GetTaikoTexts();

            public Task<TaikoText> GetTaikoTextByIdAsync(int id);

            public Task<int> StoreTaikoTextAsync(TaikoText comment);

            public int UpdateTaikoText(TaikoText comment);

            public int DeleteTaikoText(int commentId);
        }
    }
}
