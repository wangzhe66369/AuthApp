using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AuthApp.EntityFrameworkCore;
using AuthApp.Domian.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AuthApp.Domian.Auth;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.Identity
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class RoleModulePermissionRepository : Repository<RoleModulePermission, int, AppDbContext>, IRoleModulePermissionRepository
    {
        public RoleModulePermissionRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task UpdateModuleId(int permissionId, int moduleId)
        {
            var roleModulePermission = DbContext.RoleModulePermissions.Where(d=>d.PermissionId== permissionId).First();
            roleModulePermission.ModuleId = moduleId;
            await this.UpdateAsync(roleModulePermission);
        }

        public async Task<IQueryable<RoleModulePermission>> RoleModuleMaps()
        {

            var query = from rmp in DbContext.RoleModulePermissions
                        join m in DbContext.ModulePermissions on rmp.ModuleId equals m.Id into mEmpt
                        from m in mEmpt.DefaultIfEmpty()
                        join r in DbContext.Roles on rmp.RoleId equals r.Id into rEmpt
                        from r in rEmpt.DefaultIfEmpty()
                        where rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                        select new RoleModulePermission()
                        {
                            Role = r,
                            Module = m,
                            IsDeleted = rmp.IsDeleted
                        }
                        ;
            return await Task.FromResult(query);
        }
    }
}
