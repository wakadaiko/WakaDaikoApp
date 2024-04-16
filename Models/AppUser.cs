using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WakaDaikoApp.Models;

public class AppUser : IdentityUser
{
    public List<string?>? Instruments { get; set; }
    [ForeignKey("FamilyId")]
    public List<AppUser?>? Family { get; set; }

}