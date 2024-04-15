using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IRepository{
        public Task<int> AddTeamAsync(Team team);
        public Task<List<Team>> GetTeamsByNameAsync(List<string> teams);
    }
}
