using Microsoft.EntityFrameworkCore;
using System.Linq;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class Repository : IRepository
    {
        AppDbContext _db;
        #region Add Functions
        public async Task<int> CreateTeamAsync(Team team)
        {
           await _db.AddAsync(team);
            return await _db.SaveChangesAsync();
        }
        #endregion
    }
}
