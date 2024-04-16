using Microsoft.EntityFrameworkCore;
using System.Linq;
using WakaDaikoApp.Models;
using WakaDaikoApp.Data;

namespace WakaDaikoApp.Data
{
    public class Repository : IRepository
    {
        AppDbContext _db;
        public Repository(AppDbContext db) => _db = db;
        #region Add Functions

        public async Task<int> AddTeamAsync(Team team)
        {
           await _db.Teams.AddAsync(team);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> AddResource() { return 0; }

        #endregion
    }
}
