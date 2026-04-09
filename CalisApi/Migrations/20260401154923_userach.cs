using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalisApi.Migrations
{
    /// <inheritdoc />
    public partial class userach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAChievements_Achievements_AchievementId",
                table: "UserAChievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAChievements_Users_UserId",
                table: "UserAChievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAChievements",
                table: "UserAChievements");

            migrationBuilder.RenameTable(
                name: "UserAChievements",
                newName: "UserAchievements");

            migrationBuilder.RenameIndex(
                name: "IX_UserAChievements_UserId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAChievements_AchievementId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_AchievementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.RenameTable(
                name: "UserAchievements",
                newName: "UserAChievements");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAChievements",
                newName: "IX_UserAChievements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAChievements",
                newName: "IX_UserAChievements_AchievementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAChievements",
                table: "UserAChievements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAChievements_Achievements_AchievementId",
                table: "UserAChievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAChievements_Users_UserId",
                table: "UserAChievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
