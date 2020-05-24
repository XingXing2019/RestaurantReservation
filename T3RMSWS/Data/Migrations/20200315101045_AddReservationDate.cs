using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class AddReservationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationDateId",
                table: "Sittings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationDateId",
                table: "ReservationRequests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservationDates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRequests_ReservationDateId",
                table: "ReservationRequests",
                column: "ReservationDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRequests_ReservationDates_ReservationDateId",
                table: "ReservationRequests",
                column: "ReservationDateId",
                principalTable: "ReservationDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_ReservationDates_ReservationDateId",
                table: "ReservationRequests");

            migrationBuilder.DropTable(
                name: "ReservationDates");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_ReservationDateId",
                table: "ReservationRequests");

            migrationBuilder.DropColumn(
                name: "ReservationDateId",
                table: "Sittings");

            migrationBuilder.DropColumn(
                name: "ReservationDateId",
                table: "ReservationRequests");
        }
    }
}
