using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverHire.Data.Migrations
{
    public partial class tableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Otp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpExpiryDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpiryDate",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsCustomer = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickUpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtpExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    LicensePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverForm_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_UserId",
                table: "ApplicationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverForm_ApplicationUserId",
                table: "DriverForm",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "DriverForm");

            migrationBuilder.DropTable(
                name: "Register");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpiryDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiryDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
