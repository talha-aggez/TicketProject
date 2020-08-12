using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketProject.DataAccess.Migrations
{
    public partial class addImageUrlToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Tickets");
        }
    }
}
