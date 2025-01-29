using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelInfoTableAgainModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "PersonnelInfos",
                newName: "UnitInPersian");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "PersonnelInfos",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitInPersian",
                table: "PersonnelInfos",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "PersonnelInfos",
                newName: "FirstName");
        }
    }
}
