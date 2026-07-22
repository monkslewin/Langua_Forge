using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguaForge.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conjugation_Verbs_VerbId",
                table: "Conjugation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conjugation",
                table: "Conjugation");

            migrationBuilder.RenameTable(
                name: "Conjugation",
                newName: "Conjugations");

            migrationBuilder.RenameIndex(
                name: "IX_Conjugation_VerbId",
                table: "Conjugations",
                newName: "IX_Conjugations_VerbId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conjugations",
                table: "Conjugations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conjugations_Verbs_VerbId",
                table: "Conjugations",
                column: "VerbId",
                principalTable: "Verbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conjugations_Verbs_VerbId",
                table: "Conjugations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conjugations",
                table: "Conjugations");

            migrationBuilder.RenameTable(
                name: "Conjugations",
                newName: "Conjugation");

            migrationBuilder.RenameIndex(
                name: "IX_Conjugations_VerbId",
                table: "Conjugation",
                newName: "IX_Conjugation_VerbId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conjugation",
                table: "Conjugation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conjugation_Verbs_VerbId",
                table: "Conjugation",
                column: "VerbId",
                principalTable: "Verbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
