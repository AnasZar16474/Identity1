using System.ComponentModel.DataAnnotations;

namespace Identity1.Models.ViewModel
{
    public class LogInViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [EmailAddress]
        [Required]
        [Display(Name ="EmailAddress")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }

    }
}
