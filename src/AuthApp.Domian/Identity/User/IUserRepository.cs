
using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Domian.Identity
{
    /// <summary>
    /// User 仓储接口
    /// </summary>
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
