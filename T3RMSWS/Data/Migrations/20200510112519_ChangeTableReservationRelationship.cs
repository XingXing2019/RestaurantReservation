using Microsoft.EntityFrameworkCore.Migrations;

namespace T3RMSWS.Data.Migrations
{
    public partial class ChangeTableReservationRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRequests_Tables_TableId",
                table: "ReservationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRequests_TableId",
                table: "ReservationRequests");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Tables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "ReservationRequests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_ReservationRequests_ReservationId",
                table: "Tables",
                column: "ReservationId",
                principalTable: "ReservationRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_ReservationRequests_ReservationId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ReservationId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Tables");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "ReservationRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
