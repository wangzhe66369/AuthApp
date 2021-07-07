using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AuthApp.EntityFrameworkCore;
using AuthApp.Domian.Identity;

namespace AuthApp.EntityFrameworkCore.Identity
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class UserRoleRepository : Repository<UserRole, int, AppDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(AppDbContext context)
            : base(context)
        {
        }

    }
}
