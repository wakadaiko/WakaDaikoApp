using WakaDaikoApp.Models;

namespace WakaDaikoApp.Data
{
    public class FakeDrumTeamRepository : IDrumTeamRepository
    {
        readonly List<DrumTeam> _drumteam = [];

        public async Task<DrumTeam> GetDrumTeamByIdAsync(int id) => throw new NotImplementedException();

        public List<DrumTeam> GetDrumTeams() => throw new NotImplementedException();

        public async Task<int> StoreDrumTeamAsync(DrumTeam comment)
        {
            _drumteam.Add(comment);

            return comment.MemberId;
        }

        public int UpdateDrumTeam(DrumTeam comment) => throw new NotImplementedException();

        public int DeleteDrumTeam(int commentId) => throw new NotImplementedException();
    }
}
