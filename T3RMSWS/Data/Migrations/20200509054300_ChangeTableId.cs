using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class ChangeTableId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableName",
                table: "Tables");

            migrationBuilder.AddColumn<int>(
                name: "TableType",
                table: "Tables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableType",
                table: "Tables");

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
