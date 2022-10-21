using StudentSite.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentSite.Entities
{
    public class PointOperation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public PointOperationType OperationType { get; set; }
        public int PointAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
