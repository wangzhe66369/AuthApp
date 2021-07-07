using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApp.EntityFrameworkCore.Migrations
{
    public partial class addInit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                table: "SysUserRole",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SysUser",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "SysRole",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SysUserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SysUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SysRole",
                newName: "RoleId");
        }
    }
}
