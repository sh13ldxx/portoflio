using Microsoft.EntityFrameworkCore.Migrations;

namespace Portoflio.Migrations
{
    public partial class AdClTael : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Interests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Interests");
        }
    }
}
