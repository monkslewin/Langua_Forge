using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanguaForge.API.Migrations
{
    /// <inheritdoc />
    public partial class ExtendVerbModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Meaning",
                table: "Verbs",
                newName: "Group");

            migrationBuilder.AddColumn<string>(
                name: "English",
                table: "Verbs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FrequencyRank",
                table: "Verbs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsIrregular",
                table: "Verbs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReflexive",
                table: "Verbs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "English",
                table: "Verbs");

            migrationBuilder.DropColumn(
                name: "FrequencyRank",
                table: "Verbs");

            migrationBuilder.DropColumn(
                name: "IsIrregular",
                table: "Verbs");

            migrationBuilder.DropColumn(
                name: "IsReflexive",
                table: "Verbs");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "Verbs",
                newName: "Meaning");
        }
    }
}
