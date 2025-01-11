using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class ModelsModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiseQuestions",
                table: "MultipleChoiseQuestions");

            migrationBuilder.RenameTable(
                name: "MultipleChoiseQuestions",
                newName: "MultipleChoiceQuestions");

            migrationBuilder.AddColumn<string>(
                name: "SexFa",
                table: "Sexes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiceQuestions",
                table: "MultipleChoiceQuestions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MultipleChoiceQuestions",
                table: "MultipleChoiceQuestions");

            migrationBuilder.DropColumn(
                name: "SexFa",
                table: "Sexes");

            migrationBuilder.RenameTable(
                name: "MultipleChoiceQuestions",
                newName: "MultipleChoiseQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MultipleChoiseQuestions",
                table: "MultipleChoiseQuestions",
                column: "Id");
        }
    }
}
