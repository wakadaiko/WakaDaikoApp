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
        #region Get Functions
        public async Task<List<Team>> GetTeamsByNameAsync(List<string> teams) {
            var _teams = await _db.Teams.Where(t => teams.Contains(t.Name) == true).ToListAsync();
            return _teams;
        }

        #endregion

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
