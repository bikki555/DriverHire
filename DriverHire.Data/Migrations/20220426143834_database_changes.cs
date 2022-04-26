using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverHire.Data.Migrations
{
    public partial class database_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ApplicationUser_UserId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "DriverForm");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Booking",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                newName: "IX_Booking_DriverId");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingAcceptedDate",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CancelById",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CanceledDate",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApplicationUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FeedBack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DRating = table.Column<int>(type: "int", nullable: false),
                    DFeedBackMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CRating = table.Column<int>(type: "int", nullable: false),
                    CFeedBackMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBack_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CancelById",
                table: "Booking",
                column: "CancelById");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBack_BookingId",
                table: "FeedBack",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ApplicationUser_CancelById",
                table: "Booking",
                column: "CancelById",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ApplicationUser_CustomerId",
                table: "Booking",
                column: "CustomerId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ApplicationUser_DriverId",
                table: "Booking",
                column: "DriverId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ApplicationUser_CancelById",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ApplicationUser_CustomerId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_ApplicationUser_DriverId",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "FeedBack");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CancelById",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CustomerId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingAcceptedDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelById",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CanceledDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Booking",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DriverId",
                table: "Booking",
                newName: "IX_Booking_UserId");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "DriverForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_ApplicationUser_UserId",
                table: "Booking",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
