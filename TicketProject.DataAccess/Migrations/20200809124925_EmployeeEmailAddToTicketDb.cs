using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProject.DataAccess.Migrations
{
    public partial class EmployeeEmailAddToTicketDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "Tickets");
        }
    }
}
