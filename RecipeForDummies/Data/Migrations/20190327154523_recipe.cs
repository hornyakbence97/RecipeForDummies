using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeForDummies.Data.Migrations
{
    public partial class recipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Uploaded = table.Column<DateTime>(nullable: false),
                    Tumbnail = table.Column<byte[]>(nullable: false),
                    SmallDescription = table.Column<string>(nullable: false),
                    InsturctionsJson = table.Column<string>(nullable: false),
                    IngredientsJson = table.Column<string>(nullable: false),
                    ImageUrlListJson = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "RecipeComment",
                columns: table => new
                {
                    RecipeCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentText = table.Column<string>(nullable: false),
                    UploadedOrModified = table.Column<DateTime>(nullable: false),
                    ReceipeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeComment", x => x.RecipeCommentId);
                    table.ForeignKey(
                        name: "FK_RecipeComment_Recipe_ReceipeId",
                        column: x => x.ReceipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeComment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeRating",
                columns: table => new
                {
                    RecipeRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateValue = table.Column<int>(nullable: false),
                    ReceipeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRating", x => x.RecipeRatingId);
                    table.ForeignKey(
                        name: "FK_RecipeRating_Recipe_ReceipeId",
                        column: x => x.ReceipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRating_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComment_ReceipeId",
                table: "RecipeComment",
                column: "ReceipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeComment_UserId",
                table: "RecipeComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRating_ReceipeId",
                table: "RecipeRating",
                column: "ReceipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRating_UserId",
                table: "RecipeRating",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeComment");

            migrationBuilder.DropTable(
                name: "RecipeRating");

            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
