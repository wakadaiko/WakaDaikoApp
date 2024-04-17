using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeRepository : IRepository
    {
        readonly List<Event> _events = [];

        public Task<int> AddTeamAsync(Team team) => throw new NotImplementedException();

        public Task<List<Team>> GetTeamsByNameAsync(List<string> teams) => throw new NotImplementedException();

        public List<Event> GetEvents() => throw new NotImplementedException();

        public Task<Event> GetEventByIdAsync(int id) => throw new NotImplementedException();

        public async Task<int> StoreEventAsync(Event _event)
        {
            _event.EventId = _events.Count + 1;

            await Task.Run(() => _events.Add(_event));

            return _event.EventId;
        }

        public int UpdateEvent(Event _event) => throw new NotImplementedException();

        public int DeleteEvent(int _eventId) => throw new NotImplementedException();
    }
}
