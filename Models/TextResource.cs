using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    public class TextResource
    {
        [Key] public int TextId { get; set; }
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters")]

        public string? Text { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Type { get; set; }

        public string? Url { get; set; }

        public int? TextBinaryData { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
