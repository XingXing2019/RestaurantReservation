using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class RemoveTotalGuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTotalGuests",
                table: "Sittings");

            migrationBuilder.DropColumn(
                name: "ReservationDateId",
                table: "Sittings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfTotalGuests",
                table: "Sittings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationDateId",
                table: "Sittings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
