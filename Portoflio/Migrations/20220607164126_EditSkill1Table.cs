using Microsoft.EntityFrameworkCore.Migrations;

namespace Portoflio.Migrations
{
    public partial class EditSkill1Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillNames_Skills_SkillsId",
                table: "SkillNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillNames",
                table: "SkillNames");

            migrationBuilder.RenameTable(
                name: "SkillNames",
                newName: "SkillSettings");

            migrationBuilder.RenameIndex(
                name: "IX_SkillNames_SkillsId",
                table: "SkillSettings",
                newName: "IX_SkillSettings_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillSettings",
                table: "SkillSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillSettings_Skills_SkillsId",
                table: "SkillSettings",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillSettings_Skills_SkillsId",
                table: "SkillSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SkillSettings",
                table: "SkillSettings");

            migrationBuilder.RenameTable(
                name: "SkillSettings",
                newName: "SkillNames");

            migrationBuilder.RenameIndex(
                name: "IX_SkillSettings_SkillsId",
                table: "SkillNames",
                newName: "IX_SkillNames_SkillsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SkillNames",
                table: "SkillNames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SkillNames_Skills_SkillsId",
                table: "SkillNames",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
