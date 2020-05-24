using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class updateSitting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SittingId",
                table: "ReservationRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_SittingId",
                table: "ReservationRequests",
                column: "SittingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_Sittings_SittingId",
                table: "ReservationRequests",
                column: "SittingId",
                principalTable: "Sittings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Sittings_SittingId",
                table: "ReservationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_SittingId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "SittingId",
                table: "ReservationRequests");
        }
    }
}
