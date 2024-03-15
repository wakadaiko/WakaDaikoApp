using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string? Name { get; set; }

        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}
