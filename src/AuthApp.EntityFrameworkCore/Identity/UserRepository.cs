using VCrisp.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using AuthApp.EntityFrameworkCore;
using AuthApp.Domian.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.Identity
{
    /// <summary>
    /// User 仓储实现类
    /// </summary>
    public class UserRepository : Repository<User, int, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {
        }
       
    }
}
