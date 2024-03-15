using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IShimeTextRepository
    {
        public interface IBlogShimeTextRepository
        {
            public List<ShimeText> GetShimeTexts();

            public Task<ShimeText> GetShimeTextByIdAsync(int id);

            public Task<int> StoreShimeTextAsync(ShimeText comment);

            public int UpdateShimeText(ShimeText comment);

            public int DeleteShimeText(int commentId);
        }
    }
}
