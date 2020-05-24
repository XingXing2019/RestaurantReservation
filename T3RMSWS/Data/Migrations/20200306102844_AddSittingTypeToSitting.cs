using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddSittingTypeToSitting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SittingType",
                table: "Sittings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SittingType",
                table: "Sittings");
        }
    }
}
