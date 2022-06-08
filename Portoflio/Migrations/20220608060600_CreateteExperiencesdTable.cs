using Microsoft.EntityFrameworkCore.Migrations;

namespace Portoflio.Migrations
{
    public partial class CreateteExperiencesdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Expeirences");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Expeirences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

