using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverHire.Data.Migrations
{
    public partial class tablestructchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverForm_ApplicationUser_ApplicationUserId",
                table: "DriverForm");

            migrationBuilder.DropIndex(
                name: "IX_DriverForm_ApplicationUserId",
                table: "DriverForm");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DriverForm");

            migrationBuilder.DropColumn(
                name: "TokenExpiryDate",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "ApplicationUser",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAndroid",
                table: "ApplicationUser",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverForm_UserId",
                table: "DriverForm",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverForm_ApplicationUser_UserId",
                table: "DriverForm",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverForm_ApplicationUser_UserId",
                table: "DriverForm");

            migrationBuilder.DropIndex(
                name: "IX_DriverForm_UserId",
                table: "DriverForm");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "IsAndroid",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "ApplicationUser",
                newName: "Token");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "DriverForm",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiryDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DriverForm_ApplicationUserId",
                table: "DriverForm",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverForm_ApplicationUser_ApplicationUserId",
                table: "DriverForm",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
