using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesWebApp.Migrations
{
    public partial class UpdateToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieSet_GenreId",
                table: "MovieSet",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieSet_Genres_GenreId",
                table: "MovieSet",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade); // silindiğinde null değer atar
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieSet_Genres_GenreId",
                table: "MovieSet");

            migrationBuilder.DropIndex(
                name: "IX_MovieSet_GenreId",
                table: "MovieSet");
        }
    }
}
