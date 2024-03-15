using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class ShimeVideos
    {
        [Key]
        public int VideoId { get; set; }

        [ForeignKey("ConvoId")]
        public List<Comment> Comments { get; set; }

        public int? ConvoId { get; set; } = null;
    }
}
