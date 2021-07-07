using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.EntityFrameworkCore.Migrations
{
    public partial class addInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModulePermission",
                columns: table => new
                {
                    ModulePermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Area = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsMenu = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyId = table.Column<int>(type: "int", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePermission", x => x.ModulePermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pid = table.Column<int>(type: "int", nullable: false),
                    Mid = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsButton = table.Column<bool>(type: "bit", nullable: false),
                    IsHide = table.Column<bool>(type: "bit", nullable: true),
                    IskeepAlive = table.Column<bool>(type: "bit", nullable: true),
                    Func = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyId = table.Column<int>(type: "int", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "RoleModulePermission",
                columns: table => new
                {
                    RoleModulePermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyId = table.Column<int>(type: "int", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModulePermission", x => x.RoleModulePermissionId);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyId = table.Column<int>(type: "int", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserLoginName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserLoginPWD = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserRealName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserRemark = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastErrTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrorCount = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Addr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TdIsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateId = table.Column<int>(type: "int", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyId = table.Column<int>(type: "int", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "BirthDate", "BirthPlace", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), new DateTimeOffset(new DateTime(1960, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author1@xxx.com", "Author 1" },
                    { new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), new DateTimeOffset(new DateTime(1976, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Hubei", "author2@xxx.com", "Author 2" },
                    { new Guid("8406b13e-a793-4b12-84cb-7fe2a694b9aa"), new DateTimeOffset(new DateTime(1973, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Hubei", "author3@xxx.com", "Author 3" },
                    { new Guid("74556abd-1a6c-4d20-a8a7-271dd4393b2e"), new DateTimeOffset(new DateTime(1978, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author4@xxx.com", "Author 4" },
                    { new Guid("1029db57-c15c-4c0c-80a0-c811b7995cb4"), new DateTimeOffset(new DateTime(1973, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author5@xxx.com", "Author 5" },
                    { new Guid("0f978cf6-df6d-47a9-8ef2-d2723cc29cc8"), new DateTimeOffset(new DateTime(1981, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Beijing", "author6@xxx.com", "Author 6" },
                    { new Guid("10ee3976-d672-4411-ae1c-3267baa940eb"), new DateTimeOffset(new DateTime(1954, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author7@xxx.com", "Author 7" },
                    { new Guid("2633a79c-9f4a-48d5-ae5a-70945fb8583c"), new DateTimeOffset(new DateTime(1981, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Shandong", "author8@xxx.com", "Author 8" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "Description", "Pages", "Title" },
                values: new object[,]
                {
                    { new Guid("7d8ebda9-2634-4c0f-9469-0695d6132153"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 1", 281, "Book 1" },
                    { new Guid("1ed47697-aa7d-48c2-aa39-305d0e13b3aa"), new Guid("72d5b5f5-3008-49b7-b0d6-cc337f1a3330"), "Description of Book 2", 370, "Book 2" },
                    { new Guid("5f82c852-375d-4926-a3b7-84b63fc1bfae"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 3", 229, "Book 3" },
                    { new Guid("418a5b20-460b-4604-be17-2b0809e19acd"), new Guid("7d04a48e-be4e-468e-8ce2-3ac0a0c79549"), "Description of Book 4", 440, "Book 4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "ModulePermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "RoleModulePermission");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysUserRole");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
