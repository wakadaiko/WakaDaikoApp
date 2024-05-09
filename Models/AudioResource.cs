using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    public class AudioResource
    {
        [Key] public int AudioId { get; set; }
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters")]

        public string? Text { get; set; }

        public string? Title { get; set; }

        public string? Type { get; set; }

        public string? Url { get; set; }

        public int? AudioBinaryData { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
