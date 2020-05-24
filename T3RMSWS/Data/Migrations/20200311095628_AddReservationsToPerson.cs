using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddReservationsToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "ReservationRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_PersonId",
                table: "ReservationRequests",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_People_PersonId",
                table: "ReservationRequests",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_People_PersonId",
                table: "ReservationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_PersonId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ReservationRequests");
        }
    }
}
