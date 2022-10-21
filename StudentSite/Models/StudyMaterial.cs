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
    public class StudyMaterialData
    {
        public List<StudyMaterial> StudyMaterials { get; set; }
        public string SelectedCategory { get; set; }
    }
}
