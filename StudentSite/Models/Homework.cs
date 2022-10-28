using StudentSite.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentSite.Models
{
    public class HomeworkData
    {
        public int CurrentSemester { get; set; }
        public int CurrentWeek { get; set; }
        public int CurrentAmountOfWeeks { get; set; }
        public int CurrentParity { get; set; }
        public List<Schedule> Sch { get; set; }
        public List<Day> Days { get; set; }
        public List<Homework> Homeworks { get; set; }
        public List<Semester> Semesters { get; set; }
        public List<DayNote> Notes { get; set; }
    }
    public class DayNote
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
    }
    public class HomeworkWithFiles
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public string value;
        public string Value
        {
            get
            {
                if (Class != null && Class.Contains("/?Разделение на группы") && value != null)
                {
                    return value.Contains("Группа со старостой:\n") && value.Contains("Группа без старосты:\n") ? value
                    : value.Contains("Группа со старостой:") && value.Contains("Группа без старосты:") ? value.Replace("Группа со старостой:", "Группа со старостой:\n").Replace("Группа без старосты:", "Группа без старосты:\n")
                    : value != "" ? value : "Группа со старостой:\n\nГруппа без старосты:\n\n";
                }
                else if (Class != null && Class.Contains("/?Разделение на группы"))
                {
                    return "Группа со старостой:\n\nГруппа без старосты:\n\n";
                }
                else return value;
            }
            set
            {
                if (value == "Группа со старостой:\n\nГруппа без старосты:\n\n")
                    this.value = null;
                this.value = value;
            }
        }
        public IFormFileCollection Files { get; set; }
        public List<FileModel> FilesInDb { get; set; }
        public bool ReplaceImg { get; set; }
        public HomeworkWithFiles() { }
        public HomeworkWithFiles(Homework hw, ApplicationDbContext db)
        {
            if (hw != null)
            {
                value = hw.value;
                Date = hw.Date;
                Class = hw.Class;
                List<FileModel> filesInDb = new List<FileModel>();
                foreach(var fileId in hw.FileIds.Split(';'))
                {
                    if (int.TryParse(fileId, out var id))
                        filesInDb.Add(db.Files.ToList().Find(f => f.Id == id));
                }
                FilesInDb = filesInDb;
            }
            else
            {
                
            }
        }
    }
    public class Week
    {
        public int Id { get; set; }
        public int SemesterNumber { get; set; }
        public int WeekNumber { get; set; }
        public List<Day> Days { get; set; }
        public int Parity { get; set; }
    }
    public class Schedule
    {
        public int SemesterNumber { get; set; }
        public int WeekAmount { get; set; }
        public List<Day> Numerator { get; set; }
        public List<Day> Denominator { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Homework
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Class { get; set; }
        public string FileIds { get; set; }
        [Required]
        public string value;
        public string Value {
            get {
                if (Class != null && Class.Contains("/?Разделение на группы") && value != null)
                {
                    return value.Contains("Группа со старостой:\n") && value.Contains("Группа без старосты:\n") ? value
                    : value.Contains("Группа со старостой:") && value.Contains("Группа без старосты:") ? value.Replace("Группа со старостой:", "Группа со старостой:\n").Replace("Группа без старосты:", "Группа без старосты:\n")
                    : value != "" ? value : "Группа со старостой:\n\nГруппа без старосты:\n\n";
                }
                else if (Class != null && Class.Contains("/?Разделение на группы"))
                {
                    return "Группа со старостой:\n\nГруппа без старосты:\n\n";
                }
                else return value;
            }
            set
            {
                if (value == "Группа со старостой:\n\nГруппа без старосты:\n\n")
                    this.value = null;
                this.value = value;
            }
        }
    }
    public class Day
    {
        [Key]
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        [Required]
        public string Classes { get; set; }
        public DateTime Date { get; set; }
        public bool IsHoliday { get; set; }
    }
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public int SemesterNumber { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime NumeratorStart { get; set; }
        public DateTime DenominatorStart { get; set; }
    }
}
