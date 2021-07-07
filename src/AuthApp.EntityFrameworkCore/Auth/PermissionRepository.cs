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
    /// Permission 仓储实现类
    /// </summary>
    public class PermissionRepository : Repository<Permission, int, AppDbContext>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
