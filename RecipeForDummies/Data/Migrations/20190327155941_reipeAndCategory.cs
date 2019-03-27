using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class reipeAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeCategory",
                columns: table => new
                {
                    RecipeCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeCategory", x => x.RecipeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeAndCategoryConnectionTable",
                columns: table => new
                {
                    RecipeAndCategoryConnectionTableId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecipeId = table.Column<int>(nullable: false),
                    RecipeCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeAndCategoryConnectionTable", x => x.RecipeAndCategoryConnectionTableId);
                    table.ForeignKey(
                        name: "FK_RecipeAndCategoryConnectionTable_RecipeCategory_RecipeCategoryId",
                        column: x => x.RecipeCategoryId,
                        principalTable: "RecipeCategory",
                        principalColumn: "RecipeCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeAndCategoryConnectionTable_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAndCategoryConnectionTable_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable",
                column: "RecipeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAndCategoryConnectionTable_RecipeId",
                table: "RecipeAndCategoryConnectionTable",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeAndCategoryConnectionTable");

            migrationBuilder.DropTable(
                name: "RecipeCategory");
        }
    }
}
