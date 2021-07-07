using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AuthApp.EntityFrameworkCore;
using AuthApp.Domian.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AuthApp.EntityFrameworkCore.Identity
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class RoleRepository : Repository<Role, int, AppDbContext>, IRoleRepository
    {
        public RoleRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
