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
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
