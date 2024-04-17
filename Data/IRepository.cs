using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IRepository
    {
        public Task<int> AddTeamAsync(Team team);

        public Task<List<Team>> GetTeamsByNameAsync(List<string> teams);

        public List<Event> GetEvents();

        public Task<Event> GetEventByIdAsync(int id);

        public Task<int> StoreEventAsync(Event _event);

        public int UpdateEvent(Event _event);

        public int DeleteEvent(int _eventId);
    }
}
