using System.ComponentModel.DataAnnotations;

namespace StudentSite.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
