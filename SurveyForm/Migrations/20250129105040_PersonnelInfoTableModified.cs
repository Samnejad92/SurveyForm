using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelInfoTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EducationalQualification",
                table: "PersonnelInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdentificationNumber",
                table: "PersonnelInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationalQualification",
                table: "PersonnelInfos");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "PersonnelInfos");
        }
    }
}
