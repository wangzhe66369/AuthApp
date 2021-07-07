﻿// <auto-generated />
using System;
using AuthApp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthApp.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210703083219_addInit3")]
    partial class addInit3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuthApp.Domian.Auth.ModulePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ModulePermissionId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Area")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Controller")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMenu")
                        .HasColumnType("bit");

                    b.Property<string>("LinkUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("OrderSort")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ModulePermission");
                });

            modelBuilder.Entity("AuthApp.Domian.Auth.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PermissionId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Func")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Icon")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsButton")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsHide")
                        .HasColumnType("bit");

                    b.Property<bool?>("IskeepAlive")
                        .HasColumnType("bit");

                    b.Property<int>("Mid")
                        .HasColumnType("int");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrderSort")
                        .HasColumnType("int");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("AuthApp.Domian.Auth.RoleModulePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleModulePermissionId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoleModulePermission");
                });

            modelBuilder.Entity("AuthApp.Domian.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"),
                            BirthDate = new DateTimeOffset(new DateTime(1960, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Beijing",
                            Email = "author1@xxx.com",
                            Name = "Author 1"
                        },
                        new
                        {
                            Id = new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"),
                            BirthDate = new DateTimeOffset(new DateTime(1976, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Hubei",
                            Email = "author2@xxx.com",
                            Name = "Author 2"
                        },
                        new
                        {
                            Id = new Guid("8406b13e-a793-4b12-84cb-7fe2a694b9aa"),
                            BirthDate = new DateTimeOffset(new DateTime(1973, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Hubei",
                            Email = "author3@xxx.com",
                            Name = "Author 3"
                        },
                        new
                        {
                            Id = new Guid("74556abd-1a6c-4d20-a8a7-271dd4393b2e"),
                            BirthDate = new DateTimeOffset(new DateTime(1978, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Shandong",
                            Email = "author4@xxx.com",
                            Name = "Author 4"
                        },
                        new
                        {
                            Id = new Guid("1029db57-c15c-4c0c-80a0-c811b7995cb4"),
                            BirthDate = new DateTimeOffset(new DateTime(1973, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Beijing",
                            Email = "author5@xxx.com",
                            Name = "Author 5"
                        },
                        new
                        {
                            Id = new Guid("0f978cf6-df6d-47a9-8ef2-d2723cc29cc8"),
                            BirthDate = new DateTimeOffset(new DateTime(1981, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Beijing",
                            Email = "author6@xxx.com",
                            Name = "Author 6"
                        },
                        new
                        {
                            Id = new Guid("10ee3976-d672-4411-ae1c-3267baa940eb"),
                            BirthDate = new DateTimeOffset(new DateTime(1954, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Shandong",
                            Email = "author7@xxx.com",
                            Name = "Author 7"
                        },
                        new
                        {
                            Id = new Guid("2633a79c-9f4a-48d5-ae5a-70945fb8583c"),
                            BirthDate = new DateTimeOffset(new DateTime(1981, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            BirthPlace = "Shandong",
                            Email = "author8@xxx.com",
                            Name = "Author 8"
                        });
                });

            modelBuilder.Entity("AuthApp.Domian.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"),
                            AuthorId = new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"),
                            Description = "Description of Book 1",
                            Pages = 281,
                            Title = "Book 1"
                        },
                        new
                        {
                            Id = new Guid("1ed47697-aa7d-48c2-aa39-305d0e13b3aa"),
                            AuthorId = new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"),
                            Description = "Description of Book 2",
                            Pages = 370,
                            Title = "Book 2"
                        },
                        new
                        {
                            Id = new Guid("5f82c852-375d-4926-a3b7-84b63fc1bfae"),
                            AuthorId = new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"),
                            Description = "Description of Book 3",
                            Pages = 229,
                            Title = "Book 3"
                        },
                        new
                        {
                            Id = new Guid("418a5b20-460b-4604-be17-2b0809e19acd"),
                            AuthorId = new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"),
                            Description = "Description of Book 4",
                            Pages = 440,
                            Title = "Book 4"
                        });
                });

            modelBuilder.Entity("AuthApp.Domian.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifyBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderSort")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("AuthApp.Domian.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addr")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Birth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("ErrorCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastErrTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("Sex")
                        .HasColumnType("int");

                    b.Property<bool?>("TdIsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("UserLoginName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserLoginPWD")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserRealName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserRemark")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("UserStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("AuthApp.Domian.Identity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SysUserRole");
                });

            modelBuilder.Entity("AuthApp.Domian.Book", b =>
                {
                    b.HasOne("AuthApp.Domian.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("AuthApp.Domian.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
