using WakaDaikoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WakaDaikoApp.Data
{
    public class DrumTeamRepository(AppDbContext c) : IDrumTeamRepository
    {
        readonly AppDbContext _context = c;

        public async Task<DrumTeam> GetDrumTeamByIdAsync(int id)
        {
            var drumteam = await _context.DrumTeam.FindAsync(id);

            _context.Entry(drumteam).Reference(m => m.Members).Load();
            _context.Entry(drumteam).Collection(m => m.Comments).Load();

            return drumteam;
        }

        public List<DrumTeam> GetDrumTeam()
        {
            return [.. _context.DrumTeam.Include(m => m.Members)];
        }

        public async Task<int> StoreDrumTeamAsync(DrumTeam comment)
        {
            await _context.AddAsync(comment);

            return _context.SaveChanges();
        }

        public int UpdateDrumTeam(DrumTeam comment)
        {
            _context.Update(comment);

            return _context.SaveChanges();
        }

        public int DeleteDrumTeam(int commentId)
        {
            DrumTeam comment = GetDrumTeamByIdAsync(commentId).Result;

            if (comment.Comments.Count > 0) foreach (var reply in comment.Comments) _context.Comments.Remove(reply);

            _context.DrumTeam.Remove(comment);

            return _context.SaveChanges();
        }
    }
}
