using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class Repository(AppDbContext c) : IRepository
    {
        readonly AppDbContext _context = c;

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var _event = await _context.Events.FindAsync(id);

            _context.Entry(_event).Reference(m => m.Author).Load();

            return _event;
        }

        public List<Event> GetEvents()
        {
            return [.. _context.Events
            .Include(m => m.Author)
            .OrderBy(m => m.EventId)];
        }

        public async Task<int> StoreEventAsync(Event _event)
        {
            await _context.AddAsync(_event);

            return _context.SaveChanges();
        }

        public int UpdateEvent(Event _event)
        {
            _context.Update(_event);

            return _context.SaveChanges();
        }

        public int DeleteEvent(int _eventId)
        {
            Event _event = GetEventByIdAsync(_eventId).Result;

            _context.Events?.Remove(_event);

            return _context.SaveChanges();
        }
    }
}
