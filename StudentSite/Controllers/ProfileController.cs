using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentSite.Data;
using StudentSite.Data.Interfaces;
using StudentSite.Entities;
using StudentSite.Helpers;
using StudentSite.Implementations;
using StudentSite.Models;
using StudentSite.Response;
using System.Security.Claims;

namespace StudentSite.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AccountService _accountService;
        ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db, ILogger<AccountService> logger)
        {
            _db = db;
            AccountService accountService = new(db, logger);
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                var response = _accountService.Login(model);
                if (response.StatusCode == Enum.StatusCode.OK)
                {
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
        public IActionResult Account()
        {
            return View(_db);
        }

        [HttpPost]
        public IActionResult ChangePassword(string newPassword)
        {
            if(newPassword != null && newPassword.Length > 0)
            {
                User user = _db.Users.ToList().Find(x => x.Name == User.Identity?.Name);
                newPassword = Helpers.HashPasswordHelper.HashPassword(newPassword);
                User newUser = new User { Id = user.Id, Name = user.Name, Password = newPassword, Role = user.Role };
                _db.Remove(user);
                _db.Add(newUser);
                _db.SaveChanges();
            }
            return RedirectToAction("Account", "Profile");
        }

        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
