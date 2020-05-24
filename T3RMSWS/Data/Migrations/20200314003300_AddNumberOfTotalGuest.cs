using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddNumberOfTotalGuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfGuests",
                table: "Sittings");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTotalGuests",
                table: "Sittings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTotalGuests",
                table: "Sittings");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGuests",
                table: "Sittings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
