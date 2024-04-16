using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    public class Comment
    {
        [Key]
        public int CmntId { get; set; }
        [Required]
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters"),MinLength(16)]
        public string Text { get; set; }
        [Required]
        public AppUser Sndr { get; set; }
        [Required]
        public AppUser Rcpnt { get; set; }
        [ForeignKey("ConvoId")]
        public List<Comment> Comments { get; set; }

    }
}