using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalisApi.Migrations
{
    /// <inheritdoc />
    public partial class userachiev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "UserAChievements",
                newName: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAChievements_UserId",
                table: "UserAChievements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAChievements_Users_UserId",
                table: "UserAChievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAChievements_Users_UserId",
                table: "UserAChievements");

            migrationBuilder.DropIndex(
                name: "IX_UserAChievements_UserId",
                table: "UserAChievements");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "UserAChievements",
                newName: "ClassId");
        }
    }
}
