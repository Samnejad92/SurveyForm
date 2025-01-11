using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class AnswerModelModifiedAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unit",
                table: "Answers",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "sex",
                table: "Answers",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "edu",
                table: "Answers",
                newName: "Edu");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Answers",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "post",
                table: "Answers",
                newName: "WorkExperience");

            migrationBuilder.RenameColumn(
                name: "personelCode",
                table: "Answers",
                newName: "PersonnelNumber");

            migrationBuilder.RenameColumn(
                name: "exper",
                table: "Answers",
                newName: "Position");

            migrationBuilder.AddColumn<int>(
                name: "PersonnelNumber",
                table: "MultipleAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonnelNumber",
                table: "MultipleAnswers");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Answers",
                newName: "unit");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Answers",
                newName: "sex");

            migrationBuilder.RenameColumn(
                name: "Edu",
                table: "Answers",
                newName: "edu");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Answers",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "WorkExperience",
                table: "Answers",
                newName: "post");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Answers",
                newName: "exper");

            migrationBuilder.RenameColumn(
                name: "PersonnelNumber",
                table: "Answers",
                newName: "personelCode");
        }
    }
}
