using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class Repository(AppDbContext c) : IRepository
    {
        readonly AppDbContext _context = c;
    }
}
