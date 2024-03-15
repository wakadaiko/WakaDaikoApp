using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class DrumTeam
    {
        [Key]
        public AppUser TeamLead { get; set; }

        public List<AppUser> Members { get; set; }

        [ForeignKey("ConvoId")]
        public List<Comment> Comments { get; set; }

        public int? ConvoId { get; set; } = null;
    }
}
