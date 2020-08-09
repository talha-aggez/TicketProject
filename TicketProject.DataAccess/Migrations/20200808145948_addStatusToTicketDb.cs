using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProject.DataAccess.Migrations
{
    public partial class addStatusToTicketDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Tickets");
        }
    }
}
