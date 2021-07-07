using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthApp.Domian;
using AuthApp.Domian.Identity;
using VCrisp;

namespace AuthApp.Users
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;
        readonly IRoleRepository _roleRepository;
        readonly IUserRoleRepository _userRoleRepository;



        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await _userRepository.GetByConditionAsync(a => a.UserLoginName == loginName && a.UserLoginPWD == loginPwd)).FirstOrDefault();
            var roleList = await _roleRepository.GetByConditionAsync(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRoles = await _userRoleRepository.GetByConditionAsync(ur => ur.UserId == user.Id);
                if (userRoles.Count() > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.RoleName).ToArray());
                }
            }
            return roleName;
        }
    }
}

