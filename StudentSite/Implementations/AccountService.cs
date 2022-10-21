using Microsoft.EntityFrameworkCore;
using StudentSite.Data;
using StudentSite.Data.Interfaces;
using StudentSite.Entities;
using StudentSite.Enum;
using StudentSite.Helpers;
using StudentSite.Models;
using StudentSite.Response;
using System.Security.Claims;

namespace StudentSite.Implementations
{
    public class AccountService : IAccountService
    {
        //private readonly IBaseRepository<User> _userRepository;
        public readonly DbSet<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ApplicationDbContext db, ILogger<AccountService> logger)
        {
            _userRepository = db.Users;
            _logger = logger;
        }
        public BaseResponse<ClaimsIdentity> Login(Login model)
        {
            try
            {
                var user = _userRepository.FirstOrDefault(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
