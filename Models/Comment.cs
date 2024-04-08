﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters"), MinLength(16)]
        public string? Text { get; set; }

        [Required]
        public AppUser? Sndr { get; set; }

        [Required]
        public AppUser? Rcpnt { get; set; }

        public List<Comment>? Comments { get; set; }

        [ForeignKey("ConvoId")]
        public int ConvoId { get; set; }
    }
}