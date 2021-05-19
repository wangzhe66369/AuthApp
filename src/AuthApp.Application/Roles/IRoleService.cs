using AuthApp.MessageDtos;
using AuthApp.Roles.Dto;
using System.Threading.Tasks;

namespace AuthApp.Roles
{
    public interface IRoleService 
    {

        Task<MessageDto<RoleDto>> CreateAsync(CreateRoleDto input);
    }
}
