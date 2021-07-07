
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    /// UserRole 仓储接口
    /// </summary>
    public interface IUserRoleRepository : IRepository<UserRole, int>
    {
    }
}
