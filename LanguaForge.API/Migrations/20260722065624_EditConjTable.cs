using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguaForge.API.Migrations
{
    /// <inheritdoc />
    public partial class EditConjTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mood",
                table: "Conjugation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Conjugation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mood",
                table: "Conjugation");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Conjugation");
        }
    }
}
