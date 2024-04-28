using System.ComponentModel.DataAnnotations;

namespace WakaDaikoApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "The Username field is required.")]
        [StringLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
