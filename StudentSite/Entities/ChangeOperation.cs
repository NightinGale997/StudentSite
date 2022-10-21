using StudentSite.Enum;
using System.ComponentModel.DataAnnotations;

namespace StudentSite.Entities
{
    public class ChangeOperation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public ChangeOperationType OperationType { get; set; }
        public string PreviousValue { get; set; }
        public string NewValue { get; set; }
        public int HomeworkId { get; set; }
    }
}
