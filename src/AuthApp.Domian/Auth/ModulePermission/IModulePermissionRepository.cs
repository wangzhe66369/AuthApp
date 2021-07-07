
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AuthApp.Domian.Auth;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    /// ModulePermission 仓储接口
    /// </summary>
    public interface IModulePermissionRepository : IRepository<ModulePermission, int>
    {
    }
}
