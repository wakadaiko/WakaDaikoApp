using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    public class VideoResource
    {
        [Key] public int VideoId { get; set; }
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters")]

        public string? Text { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Type { get; set; }

        public string? Url { get; set; }

        public int? VideoBinaryData { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
