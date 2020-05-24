using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class RemoveSittingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SittingId",
                table: "ReservationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
