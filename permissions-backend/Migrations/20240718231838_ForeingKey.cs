using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace permissions_backend.Migrations
{
    public partial class ForeingKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoPermiso",
                table: "Permissions",
                newName: "PermissionType");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "PermissionTypes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NombreEmpleado",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoEmpleado",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionType",
                table: "Permissions",
                column: "PermissionType");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionType",
                table: "Permissions",
                column: "PermissionType",
                principalTable: "PermissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionType",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionType",
                table: "Permissions");

            migrationBuilder.RenameColumn(
                name: "PermissionType",
                table: "Permissions",
                newName: "TipoPermiso");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "PermissionTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "NombreEmpleado",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoEmpleado",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
