using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverHire.Data.Migrations
{
    public partial class IsResetAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReset",
                table: "Register",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReset",
                table: "Register");
        }
    }
}
