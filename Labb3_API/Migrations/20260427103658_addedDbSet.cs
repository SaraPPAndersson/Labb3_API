using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb3_API.Migrations
{
    /// <inheritdoc />
    public partial class addedDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_UserInterest_UserInterestId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterest_Interests_InterestId",
                table: "UserInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterest_Users_UserId",
                table: "UserInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterest",
                table: "UserInterest");

            migrationBuilder.RenameTable(
                name: "UserInterest",
                newName: "UserInterests");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterest_UserId",
                table: "UserInterests",
                newName: "IX_UserInterests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterest_InterestId",
                table: "UserInterests",
                newName: "IX_UserInterests_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_UserInterests_UserInterestId",
                table: "Links",
                column: "UserInterestId",
                principalTable: "UserInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_UserInterests_UserInterestId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests");

            migrationBuilder.RenameTable(
                name: "UserInterests",
                newName: "UserInterest");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_UserId",
                table: "UserInterest",
                newName: "IX_UserInterest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_InterestId",
                table: "UserInterest",
                newName: "IX_UserInterest_InterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterest",
                table: "UserInterest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_UserInterest_UserInterestId",
                table: "Links",
                column: "UserInterestId",
                principalTable: "UserInterest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterest_Interests_InterestId",
                table: "UserInterest",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterest_Users_UserId",
                table: "UserInterest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
