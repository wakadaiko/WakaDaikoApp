using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeEventRepository : IEventRepository
    {
        readonly List<Event> _events = [];

        public async Task<Event> GetEventByIdAsync(int id) => throw new NotImplementedException();

        public List<Event> GetEvents() => throw new NotImplementedException();

        public async Task<int> StoreEventAsync(Event _event)
        {
            _event.EventId = _events.Count + 1;

            _events.Add(_event);

            return _event.EventId;
        }

        public int UpdateEvent(Event _event) => throw new NotImplementedException();

        public int DeleteEvent(int _eventId) => throw new NotImplementedException();
    }
}
