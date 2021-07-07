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
    public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");
            builder.Property(p => p.Id).HasColumnName("PermissionId");
            builder.Property(p => p.Code).HasMaxLength(50); ;
            builder.Property(p => p.Name).HasMaxLength(100); ;
            builder.Property(p => p.Func).HasMaxLength(100);
            builder.Property(p => p.Icon).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.CreateBy).HasMaxLength(50);
            builder.Property(p => p.ModifyBy).HasMaxLength(50);
        }
    }
}
