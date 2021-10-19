using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddQuestionTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "Questions",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "Questions");
        }
    }
}
