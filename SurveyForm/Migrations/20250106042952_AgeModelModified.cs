using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyForm.Migrations
{
    /// <inheritdoc />
    public partial class AgeModelModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Ages");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "WorkExperiences",
                newName: "Range");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Ages",
                newName: "Range");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Range",
                table: "WorkExperiences",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "Range",
                table: "Ages",
                newName: "To");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Ages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
