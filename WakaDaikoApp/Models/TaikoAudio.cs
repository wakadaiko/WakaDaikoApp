using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class TaikoAudio
    {
        [Key]
        public int AudioId { get; set; }

        [ForeignKey("ConvoId")]
        public List<Comment> Comments { get; set; }

        public int? ConvoId { get; set; } = null;
    }
}
