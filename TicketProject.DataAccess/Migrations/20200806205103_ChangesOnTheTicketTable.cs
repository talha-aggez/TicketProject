using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProject.DataAccess.Migrations
{
    public partial class ChangesOnTheTicketTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonelTicketId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "lockTicketForUser",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PersonelTicketId",
                table: "Tickets",
                column: "PersonelTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_PersonelTicketId",
                table: "Tickets",
                column: "PersonelTicketId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_PersonelTicketId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PersonelTicketId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PersonelTicketId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "lockTicketForUser",
                table: "Tickets");
        }
    }
}
