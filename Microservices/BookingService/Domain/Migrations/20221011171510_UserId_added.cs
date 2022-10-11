using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravixBackend.BookingService.Domain.Migrations
{
    public partial class UserId_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "bookings",
                newName: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "bookings",
                newName: "UserId");
        }
    }
}
