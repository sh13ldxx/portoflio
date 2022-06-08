using Microsoft.EntityFrameworkCore.Migrations;

namespace Portoflio.Migrations
{
    public partial class CreateteExperiencesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expeirences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    PositonTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expeirences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expeirences");
        }
    }
}
