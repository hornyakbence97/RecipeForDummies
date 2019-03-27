using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class recipeAndImageConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_Categorie_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorie",
                table: "Categorie");

            migrationBuilder.DropColumn(
                name: "ImageUrlListJson",
                table: "Recipe");

            migrationBuilder.RenameTable(
                name: "Categorie",
                newName: "RecipeCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory",
                column: "RecipeCategoryId");

            migrationBuilder.CreateTable(
                name: "ImageAndRecipeConnection",
                columns: table => new
                {
                    ImageAndRecipeConnectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecipeId = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAndRecipeConnection", x => x.ImageAndRecipeConnectionId);
                    table.ForeignKey(
                        name: "FK_ImageAndRecipeConnection_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageAndRecipeConnection_RecipeId",
                table: "ImageAndRecipeConnection",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_RecipeCategory_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategory",
                principalColumn: "RecipeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_RecipeCategory_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable");

            migrationBuilder.DropTable(
                name: "ImageAndRecipeConnection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "Categorie");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlListJson",
                table: "Recipe",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorie",
                table: "Categorie",
                column: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_Categorie_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable",
                column: "RecipeCategoryId",
                principalTable: "Categorie",
                principalColumn: "RecipeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
