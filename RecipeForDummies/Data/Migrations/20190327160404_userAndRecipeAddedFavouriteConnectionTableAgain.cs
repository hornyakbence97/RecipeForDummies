using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class userAndRecipeAddedFavouriteConnectionTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAndRecipeAddedToFavouriteConnectionTable",
                columns: table => new
                {
                    UserAndRecipeAddedToFavouriteConnectionTableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecipeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAndRecipeAddedToFavouriteConnectionTable", x => x.UserAndRecipeAddedToFavouriteConnectionTableId);
                    table.ForeignKey(
                        name: "FK_UserAndRecipeAddedToFavouriteConnectionTable_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAndRecipeAddedToFavouriteConnectionTable_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRecipeAddedToFavouriteConnectionTable_RecipeId",
                table: "UserAndRecipeAddedToFavouriteConnectionTable",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAndRecipeAddedToFavouriteConnectionTable_UserId",
                table: "UserAndRecipeAddedToFavouriteConnectionTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAndRecipeAddedToFavouriteConnectionTable");
        }
    }
}
