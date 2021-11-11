using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmStudio.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Movies_MovieId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Studios_StudioId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "StudioId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Movies_MovieId",
                table: "Ratings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Studios_StudioId",
                table: "Ratings",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Movies_MovieId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Studios_StudioId",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "StudioId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Movies_MovieId",
                table: "Ratings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Studios_StudioId",
                table: "Ratings",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
