using AuthApp.Domian;
using AuthApp.Domian.Auth;
using AuthApp.Domian.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.EntityFrameworkCore.EntityTypeConfiguration
{
    public class RoleModulePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RoleModulePermission>
    {
        public void Configure(EntityTypeBuilder<RoleModulePermission> builder)
        {
            builder.ToTable("RoleModulePermission");
            builder.Property(p => p.Id).HasColumnName("RoleModulePermissionId");
            builder.Property(p => p.CreateBy).HasMaxLength(50);
            builder.Property(p => p.ModifyBy).HasMaxLength(50);
        }
    }
}
