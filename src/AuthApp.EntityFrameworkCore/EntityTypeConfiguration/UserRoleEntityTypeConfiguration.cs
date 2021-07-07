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
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("SysUserRole").HasKey(p => p.Id);
            //builder.Property(p => p.Id).HasColumnName("UserRoleId");
            builder.Property(p => p.CreateBy).HasMaxLength(50);
            builder.Property(p => p.ModifyBy).HasMaxLength(50);
        }
    }
}
