using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddSittingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SittingType",
                table: "ReservationRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SittingType",
                table: "ReservationRequests");
        }
    }
}
