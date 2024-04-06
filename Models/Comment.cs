﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        [Required]
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters"),MinLength(16)]
        public string Text { get; set; }
        [Required]
        public AppUser Sndr { get; set; }
        [Required]
        public AppUser Rcpnt { get; set; }
        public List<Comment> Comments { get; set; }
        [ForeignKey("ConvoId")]
        public int ConvoId { get; set; }
    }
}
