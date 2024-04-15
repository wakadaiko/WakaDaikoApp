using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IRepository{
        public Task<int> AddTeamAsync(Team team);
    }
}
