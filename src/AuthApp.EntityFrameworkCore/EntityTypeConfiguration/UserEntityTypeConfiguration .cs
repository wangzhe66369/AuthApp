using AuthApp.Domian;
using AuthApp.Domian.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.EntityTypeConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("SysUser").HasKey(p => p.Id);
            //builder.Property(p => p.Id).HasColumnName("UserId");
            builder.Property(p => p.UserLoginName).HasMaxLength(200);
            builder.Property(p => p.UserLoginPWD).HasMaxLength(200);
            builder.Property(p => p.UserRealName).HasMaxLength(200);
            builder.Property(p => p.UserRemark).HasMaxLength(2000);
            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.Addr).HasMaxLength(200);
        }
    }
}
