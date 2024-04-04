using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IEventRepository
    {
        public interface IBlogEventRepository
        {
            public List<Event> GetEvents();

            public Task<Event> GetEventByIdAsync(int id);

            public Task<int> StoreEventAsync(Event _event);

            public int UpdateEvent(Event _event);

            public int DeleteEvent(int _eventId);
        }
    }
}
