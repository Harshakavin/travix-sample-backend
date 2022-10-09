using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravixBackend.BookingService.Domain.Migrations
{
    public partial class added_new_coloums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cost",
                table: "bookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "bookings",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_date",
                value: new DateTime(2022, 10, 9, 16, 36, 59, 885, DateTimeKind.Utc).AddTicks(96));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "date",
                table: "bookings");

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id",
                keyValue: 1L,
                column: "created_date",
                value: new DateTime(2022, 10, 9, 12, 47, 43, 8, DateTimeKind.Utc).AddTicks(4711));
        }
    }
}
