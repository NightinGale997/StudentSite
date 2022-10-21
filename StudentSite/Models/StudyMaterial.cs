using System.ComponentModel.DataAnnotations;

namespace StudentSite.Models
{
    public class StudyMaterial
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Reference { get; set; }
    }
}
