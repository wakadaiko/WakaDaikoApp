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
    }
}
