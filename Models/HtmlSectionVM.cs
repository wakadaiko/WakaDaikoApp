namespace WakaDaikoApp.Models
{
    public class HtmlSectionVM
    {
        public string? Type { get; set; } = string.Empty;

        public string? Image { get; set; } = string.Empty;

        public string? Heading { get; set; } = string.Empty;

        public string? Body { get; set; } = string.Empty;

        public string? URL1 { get; set; } = string.Empty;

        public string? URL2 { get; set; } = string.Empty;

        public bool? Flip { get; set; } = false;
    }
}
