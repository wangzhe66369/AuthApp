using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AuthApp.EntityFrameworkCore;
using AuthApp.Domian.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AuthApp.Domian.Auth;

namespace AuthApp.EntityFrameworkCore.Identity
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class ModulePermissionRepository : Repository<ModulePermission, int, AppDbContext>, IModulePermissionRepository
    {
        public ModulePermissionRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
