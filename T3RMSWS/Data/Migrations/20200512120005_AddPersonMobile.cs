using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddPersonMobile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "People");
        }
    }
}
