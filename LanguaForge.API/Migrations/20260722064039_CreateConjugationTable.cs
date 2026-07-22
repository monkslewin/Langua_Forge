using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguaForge.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateConjugationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conjugation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tense = table.Column<string>(type: "TEXT", nullable: false),
                    Person = table.Column<string>(type: "TEXT", nullable: false),
                    Form = table.Column<string>(type: "TEXT", nullable: false),
                    VerbId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conjugation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conjugation_Verbs_VerbId",
                        column: x => x.VerbId,
                        principalTable: "Verbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conjugation_VerbId",
                table: "Conjugation",
                column: "VerbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conjugation");
        }
    }
}
