using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public interface IDrumTeamRepository
    {
        public interface IBlogDrumTeamRepository
        {
            public List<DrumTeam> GetDrumTeams();

            public Task<DrumTeam> GetDrumTeamByIdAsync(int id);

            public Task<int> StoreDrumTeamAsync(DrumTeam comment);

            public int UpdateDrumTeam(DrumTeam comment);

            public int DeleteDrumTeam(int commentId);
        }
    }
}
