
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AuthApp.Domian.Auth;
using System.Threading.Tasks;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    ///Permission 仓储接口
    /// </summary>
    public interface IRoleModulePermissionRepository : IRepository<RoleModulePermission, int>
    {
        Task UpdateModuleId(int permissionId, int moduleId);
        Task<IQueryable<RoleModulePermission>> RoleModuleMaps();
    }
}
