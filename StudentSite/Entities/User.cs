using StudentSite.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentSite.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
