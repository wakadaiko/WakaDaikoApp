using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WakaDaikoApp.Models;

public class AppUser : IdentityUser
{
    public List<string?> Instruments { get; set; }
    public List<Team> Teams { get; set; }
    public List<AppUser?> Dependants { get; set; }
    [ForeignKey("FamilyId")]
    public int FamilyId { get; set; }

}