using Microsoft.AspNetCore.Mvc;
using StudentSite.Data;
using StudentSite.Entities;

namespace StudentSite.Controllers
{
    public class AdminPanelController : Controller
    {
        ApplicationDbContext _db;
        UsersData _usersData;
        
        public AdminPanelController(ApplicationDbContext db)
        {
            _db = db;
            _usersData = new UsersData
            {
                Users = _db.Users.ToList(),
                PointOperations = _db.PointOperations.ToList(),
                ChangeOperations = _db.ChangeOperations.ToList()
            };
        }

        public IActionResult Index()
        {
            return View(_usersData);
        }

    }
}
