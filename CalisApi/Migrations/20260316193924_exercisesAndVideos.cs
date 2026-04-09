using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalisApi.Migrations
{
    /// <inheritdoc />
    public partial class exercisesAndVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "RutineExercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RutineExercises_VideoId",
                table: "RutineExercises",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RutineExercises_Videos_VideoId",
                table: "RutineExercises",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RutineExercises_Videos_VideoId",
                table: "RutineExercises");

            migrationBuilder.DropIndex(
                name: "IX_RutineExercises_VideoId",
                table: "RutineExercises");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "RutineExercises");
        }
    }
}
