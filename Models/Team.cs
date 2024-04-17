using System.ComponentModel.DataAnnotations;

namespace WakaDaikoApp.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string? Name { get; set; }

        public AppUser? TeamLead { get; set; }

        public string? Description { get; set; }

        public List<string>? Instruments { get; set; } = [];

        public List<AppUser>? Members { get; set; } = [];

        public List<string>? Positions { get; set; } = [];
    }
}
