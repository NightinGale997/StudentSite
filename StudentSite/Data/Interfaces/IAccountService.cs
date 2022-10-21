using StudentSite.Models;
using StudentSite.Response;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentSite.Data.Interfaces
{
    public interface IAccountService
    {

        BaseResponse<ClaimsIdentity> Login(Login model);

        //Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}
