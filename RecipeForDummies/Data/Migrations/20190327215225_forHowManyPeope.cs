using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class forHowManyPeope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForHowManyPeople",
                table: "Recipe",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForHowManyPeople",
                table: "Recipe");
        }
    }
}
