using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakaDaikoApp.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateOnly Date { get; set; }

        public AppUser Author { get; set; }

        [ForeignKey("ConvoId")]
        public List<Comment> Comments { get; set; } = new();

        public int? ConvoId { get; set; } = null;
    }
}
