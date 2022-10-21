using Microsoft.AspNetCore.Mvc;
using StudentSite.Data;
using StudentSite.Models;

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
            var objStudyMaterialsList = _db.StudyMaterials.ToList();

            var studyMaterials = new StudyMaterialData
            {
                StudyMaterials = objStudyMaterialsList,
                SelectedCategory = objStudyMaterialsList[0].Category
            };

            return View(studyMaterials);
        }
        //[HttpPost]
        //public IActionResult Index(string ItemIds)
        //{
        //    var categoryNames = !string.IsNullOrEmpty(ItemIds) ? ItemIds.Split(',').ToList() : new List<string>();
        //    var objStudyMaterials = _db.StudyMaterials.ToList().Where(x => categoryNames.Contains(x.Category.Replace(' ', '-'))).ToList();
        //    var objStudyMaterialsList = new List<Models.StudyMaterial>[2] { _db.StudyMaterials.ToList(), objStudyMaterials };
        //    return PartialView(objStudyMaterialsList);
        //}
        [HttpPost]
        public IActionResult Index(string ItemIds)
        {
            var category = ItemIds.Replace("-", " ");

            var objStudyMaterialsList = _db.StudyMaterials.ToList();
            var studyMaterials = new StudyMaterialData
            {
                StudyMaterials = objStudyMaterialsList,
                SelectedCategory = category
            };

            return PartialView(studyMaterials);
        }
    }
}
