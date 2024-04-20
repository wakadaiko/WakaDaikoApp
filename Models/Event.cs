using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Description must be between 1 - 50 characters"), MinLength(1)]
        public string? Description { get; set; }

        public string? Image { get; set; } = "favicon.jpg";

        [Required]
        [StringLength(50, ErrorMessage = "Text must be between 1 - 50 characters"), MinLength(1)]
        public string? Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Text must be between 1 - 1000 characters"), MinLength(1)]
        public string? Text { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public AppUser? Author { get; set; }

        public bool Pinned { get; set; }

        public List<Comment>? Comments { get; set; }

        [ForeignKey("ConvoId")]
        public int ConvoId { get; set; }
    }
}
