using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class recipeCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_RecipeCategory_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory");

            migrationBuilder.RenameTable(
                name: "RecipeCategory",
                newName: "Categorie");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_Categorie_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorie",
                table: "Categorie");

            migrationBuilder.RenameTable(
                name: "Categorie",
                newName: "RecipeCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeCategory",
                table: "RecipeCategory",
                column: "RecipeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeAndCategoryConnectionTable_RecipeCategory_RecipeCategoryId",
                table: "RecipeAndCategoryConnectionTable",
                column: "RecipeCategoryId",
                principalTable: "RecipeCategory",
                principalColumn: "RecipeCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
