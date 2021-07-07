using AuthApp.MessageDtos;
using AuthApp.Roles.Dto;
using System.Threading.Tasks;

namespace AuthApp.Users
{
    public interface IUserService 
    {

        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);

    }
}
