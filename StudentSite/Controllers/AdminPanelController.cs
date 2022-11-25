using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public IActionResult EditUser(string userString)
        {
            string[] userVars = userString?.Split("/../");
            if (userString != null && userVars.Length == 4)
            {
                User model = new User();
                model.Id = int.Parse(userVars[0]);
                model.Name = userVars[1];
                model.Role = (Enum.Role)int.Parse(userVars[2]);
                model.Password = userVars[3];
                return PartialView(model);
            }
            else if(userString == "newUser")
            {
                User model = new User();
                model.Name = "";
                model.Role = (Enum.Role)0;
                model.Password = "";
                return PartialView(model);
            }
            return null;
        }
        public IActionResult DeleteUser(string userString)
        {
            string[] userVars = userString?.Split("/../");
            if (userString != null && userVars.Length == 4)
            {
                User model = new User();
                model.Id = int.Parse(userVars[0]);
                model.Name = userVars[1];
                model.Role = (Enum.Role)int.Parse(userVars[2]);
                model.Password = userVars[3];
                return PartialView(model);
            }
            else if (userString == "newUser")
            {
                User model = new User();
                model.Name = "";
                model.Role = (Enum.Role)0;
                model.Password = "";
                return PartialView(model);
            }
            return null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {
            var sameUser = _db.Users.ToList().Find(x => x.Id == user.Id);
            if (user.Password != null && user.Password.Length > 0)
                user.Password = Helpers.HashPasswordHelper.HashPassword(user.Password);
            if (sameUser != null)
            {
                if (user.Password == null || user.Password.Length < 1)
                    user.Password = sameUser.Password;
                    
                _db.Users.Remove(sameUser);
            }

            _db.Users.Add(user);

            _db.SaveChanges();

            return RedirectToAction("Index", "AdminPanel");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(User user)
        {
            var sameUser = _db.Users.ToList().Find(x => x.Id == user.Id);
            if (sameUser == null)
            {
                return RedirectToAction("Index", "AdminPanel");
            }
            _db.Users.Remove(sameUser);
            _db.SaveChanges();
            return RedirectToAction("Index", "AdminPanel");
        }
    }
}
