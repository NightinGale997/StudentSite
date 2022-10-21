using System.ComponentModel.DataAnnotations;

namespace StudentSite.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(20, ErrorMessage = "Логин должен иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должен иметь длину больше 3 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
