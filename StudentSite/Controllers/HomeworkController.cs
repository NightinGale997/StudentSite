using Microsoft.AspNetCore.Mvc;
using StudentSite.Data;
using StudentSite.Enum;
using StudentSite.Models;

namespace StudentSite.Controllers
{
    public class HomeworkController : Controller
    {
        public readonly ApplicationDbContext _db;
        HomeworkData data;

        public HomeworkController(ApplicationDbContext db) => _db = db;
        public IActionResult Index()
        {
            var semesters = _db.Semesters.ToList();
            var days = _db.Days.ToList();
            var hw = _db.Homeworks.ToList();
            data = new HomeworkData();
            data.Sch = new List<Schedule>();
            data.Days = days;
            data.Homeworks = hw;
            data.Semesters = semesters;
            data.CurrentSemester = semesters.Find(s => s.DateStart < DateTime.Now && s.DateEnd > DateTime.Now).SemesterNumber;
            data.CurrentParity = ((int)(DateTime.Now - semesters[data.CurrentSemester - 1].NumeratorStart).TotalDays / 7) % 2 == 1 ? 1 : 0;
            var daysFromSemesterStart = (int)(DateTime.Now - semesters[data.CurrentSemester - 1].DateStart).TotalDays;
            data.CurrentWeek = daysFromSemesterStart / 7 + 1;

            for (int i = 0; i < semesters.Count; i++)
            {
                data.Sch.Add(new Schedule
                {
                    SemesterNumber = semesters[i].SemesterNumber,
                    WeekAmount = (int)(semesters[i].DateEnd - semesters[i].DateStart).TotalDays / 7 + (((int)(semesters[i].DateEnd - semesters[i].DateStart).TotalDays) % 7 > 0 ? 1 : 0),
                    Numerator = days.Where(d => d.Date >= semesters[i].NumeratorStart && semesters[i].NumeratorStart.AddDays(5) >= d.Date).ToList(),
                    Denominator = days.Where(d => d.Date >= semesters[i].DenominatorStart && semesters[i].DenominatorStart.AddDays(5) >= d.Date).ToList()
                });
            }

            data.CurrentAmountOfWeeks = data.Sch[data.CurrentSemester - 1].WeekAmount;

            return View(data);
        }
        [HttpPost]
        public IActionResult Index(string ItemIds)
        {
            var numbers = !string.IsNullOrEmpty(ItemIds) ? ItemIds.Split(',').ToList() : new List<string>();
            var semesters = _db.Semesters.ToList();
            var days = _db.Days.ToList();
            var hw = _db.Homeworks.ToList();
            data = new HomeworkData();
            data.Sch = new List<Schedule>();
            data.Days = days;
            data.Homeworks = hw;
            data.Semesters = semesters;
            for (int i = 0; i < semesters.Count; i++)
            {
                data.Sch.Add(new Schedule
                {
                    SemesterNumber = semesters[i].SemesterNumber,
                    WeekAmount = (int)(semesters[i].DateEnd - semesters[i].DateStart).TotalDays / 7 + (((int)(semesters[i].DateEnd - semesters[i].DateStart).TotalDays) % 7 > 0 ? 1 : 0),
                    Numerator = days.Where(d => d.Date >= semesters[i].NumeratorStart && semesters[i].NumeratorStart.AddDays(5) >= d.Date).ToList(),
                    Denominator = days.Where(d => d.Date >= semesters[i].DenominatorStart && semesters[i].DenominatorStart.AddDays(5) >= d.Date).ToList()
                });
            }
            data.CurrentSemester = int.Parse(numbers[0]);
            data.CurrentWeek = int.Parse(numbers[1]);
            data.CurrentParity = ((int)(data.Semesters[data.CurrentSemester - 1].DateStart.AddDays(7 * (data.CurrentWeek-1) + 14) - data.Semesters[data.CurrentSemester - 1].NumeratorStart).TotalDays / 7) % 2 == 1 ? 1 : 0;
            data.CurrentAmountOfWeeks = data.Sch[data.CurrentSemester - 1].WeekAmount;
            return PartialView(data);
        }
        public IActionResult GetHomework(string hwString)
        {
            var hwData = hwString.Replace("|", "\r\n").Split("/../");
            var hw = _db.Homeworks.ToList();
            var model = hw.Find(x => x.Id == int.Parse(hwData[0]));
            return PartialView(model);
        }

        //GET
        public IActionResult EditHomework(string hwString)
        {
            var hwData = hwString.Replace("|", "\r\n").Split("/../");
            var hw = _db.Homeworks.ToList();
            int id;
            if (!int.TryParse(hwData[0], out id)) id = -1;
            var model = hw.Find(x => x.Id == id);
            if (model == null)
            {
                model = new Homework
                {
                    Date = DateTime.Parse(hwData[1]),
                    Class = hwData[2]
                };
            }
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHomework(Homework homework)
        {
            bool isValuable = true;
            string value = "";
            string newValue = homework.Value + "";
            var homeworks = _db.Homeworks.ToList();
            var sameHomework = homeworks.Find(x => x.Date == homework.Date && x.Class == homework.Class);
            var currentUser = _db.Users.ToList().Find(x => x.Name == User.Identity.Name);

            if (sameHomework != null)
            {
                value = sameHomework.Value;
                if (_db.ChangeOperations.ToList().Find(x => x.HomeworkId == sameHomework.Id) != null)
                    isValuable = false;
                //_db.Homeworks.Remove(sameHomework);
                _db.Homeworks.Find(sameHomework.Id).Value = newValue;
            }

            ChangeOperationType opType = sameHomework == null ? ChangeOperationType.WriteHomework : ChangeOperationType.EditHomework;

            if (opType == ChangeOperationType.WriteHomework && _db.ChangeOperations.ToList().Find(x => x.UserId == currentUser.Id && x.HomeworkId == homework.Id) == null)
            {
                _db.PointOperations.Add(
                    new Entities.PointOperation
                    {
                        UserId = currentUser.Id,
                        PointAmount = 1,
                        OperationType = PointOperationType.Get,
                        Date = DateTime.Now
                    }
                    );
            }

            if (homework.Value != null && homework.Value.Length > 0 && sameHomework == null && homework.Value != "Группа со старостой:\n\nГруппа без старосты:\n\n")
            {
                _db.Homeworks.Add(homework);
            }

            _db.SaveChanges();

            _db.ChangeOperations.Add(new Entities.ChangeOperation
            {
                HomeworkId = _db.Homeworks.ToList().Find(x => x.Date == homework.Date && x.Class == homework.Class) == null ? sameHomework.Id :
                _db.Homeworks.ToList().Find(x => x.Date == homework.Date && x.Class == homework.Class).Id,
                UserId = currentUser.Id,
                OperationType = opType,
                PreviousValue = value,
                NewValue = newValue
            });

            _db.SaveChanges();
            return RedirectToAction("Index", "Homework");
        }
    }
}
