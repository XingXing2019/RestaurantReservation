using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddTableReservationRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "ReservationRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_TableId",
                table: "ReservationRequests",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Tables_TableId",
                table: "ReservationRequests",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Tables_TableId",
                table: "ReservationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_TableId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "ReservationRequests");
        }
    }
}
