using Microsoft.EntityFrameworkCore.Migrations;

namespace Transaction.Api.Migrations
{
    public partial class AddedNewTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResetPasswordAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    FirstQuestionAns = table.Column<string>(nullable: true),
                    SecondQuestionAns = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordAnswers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetPasswordAnswers");
        }
    }
}
