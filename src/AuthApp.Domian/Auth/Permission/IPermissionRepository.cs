
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AuthApp.Domian.Auth;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    ///Permission 仓储接口
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission, int>
    {
    }
}
