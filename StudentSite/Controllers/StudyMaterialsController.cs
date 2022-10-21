using Microsoft.AspNetCore.Mvc;
using StudentSite.Data;

namespace StudentSite.Controllers
{
    public class StudyMaterialsController : Controller
    {
        public readonly ApplicationDbContext _db;

        public StudyMaterialsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objStudyMaterialsList = new List<Models.StudyMaterial>[2] { _db.StudyMaterials.ToList(), new List<Models.StudyMaterial>() };
            return View(objStudyMaterialsList);
        }
        [HttpPost]
        public IActionResult Index(string ItemIds)
        {
            var categoryNames = !string.IsNullOrEmpty(ItemIds) ? ItemIds.Split(',').ToList() : new List<string>();
            var objStudyMaterials = _db.StudyMaterials.ToList().Where(x => categoryNames.Contains(x.Category.Replace(' ', '-'))).ToList();
            var objStudyMaterialsList = new List<Models.StudyMaterial>[2] { _db.StudyMaterials.ToList(), objStudyMaterials };
            return PartialView(objStudyMaterialsList);
        }
    }
}
