using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeRepository : IRepository
    {
        readonly List<Comment> _comments = [];
    }
}
