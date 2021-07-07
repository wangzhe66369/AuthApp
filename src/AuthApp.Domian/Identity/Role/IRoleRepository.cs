
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    /// Role 仓储接口
    /// </summary>
    public interface IRoleRepository : IRepository<Role, int>
    {
    }
}
