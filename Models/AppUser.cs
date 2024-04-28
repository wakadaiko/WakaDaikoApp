using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }

        public List<string>? Instruments { get; set; }

        [ForeignKey("FamilyId")]
        public List<AppUser>? Family { get; set; }

        [NotMapped]
        public IList<string>? RoleNames { get; set; }

        public List<int>? MetronomePreferances { get; set; } = new List<int> { 100, 90, 120, 80, 50 };
    }
}
