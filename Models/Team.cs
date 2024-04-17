﻿using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace WakaDaikoApp.Models
{
    public class Team{
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string? Name { get; set; }
        public AppUser? TeamLead { get; set; }
        public string? Description { get; set; }
        public List<string?>? Instruments { get; set; } = new List<string?>();
        public List<AppUser?>? Members { get; set; } = new List<AppUser?>();
        public List<string?>? Positions { get; set; } = new List<string?>();
    }
}