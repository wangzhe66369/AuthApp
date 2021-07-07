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
    public class ModulePermissionEntityTypeConfiguration : IEntityTypeConfiguration<ModulePermission>
    {
        public void Configure(EntityTypeBuilder<ModulePermission> builder)
        {
            builder.ToTable("ModulePermission");
            builder.Property(p => p.Id).HasColumnName("ModulePermissionId");
            builder.Property(p => p.Name).HasMaxLength(50); ;
            builder.Property(p => p.LinkUrl).HasMaxLength(100); ;
            builder.Property(p => p.Area).HasMaxLength(2000);
            builder.Property(p => p.Controller).HasMaxLength(2000);
            builder.Property(p => p.Action).HasMaxLength(2000);
            builder.Property(p => p.Icon).HasMaxLength(100);
            builder.Property(p => p.Code).HasMaxLength(10);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.CreateBy).HasMaxLength(50);
            builder.Property(p => p.ModifyBy).HasMaxLength(100);
        }
    }
}
