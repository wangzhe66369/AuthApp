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
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("SysRole");
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.Id).HasColumnName("RoleId");
            builder.Property(p => p.RoleName).HasMaxLength(50); ;
            builder.Property(p => p.Description).HasMaxLength(100); ;
            builder.Property(p => p.CreateBy).HasMaxLength(50);
        }
    }
}
