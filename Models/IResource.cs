using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace WakaDaikoApp.Models
{
    [NotMapped]
    public class IResource
    {
        [StringLength(254, ErrorMessage = "Text must be between 0 - 254 characters")]

        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        public string Url { get; set; }
        public Blob BlobData { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
