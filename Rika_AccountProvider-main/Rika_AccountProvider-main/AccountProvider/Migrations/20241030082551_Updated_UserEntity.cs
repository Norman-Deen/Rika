using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountProvider.Migrations
{
    /// <inheritdoc />
    public partial class Updated_UserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "Users",
                newName: "isEmailConfirmed");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "isEmailConfirmed",
                table: "Users",
                newName: "EmailConfirmed");
        }
    }
}
