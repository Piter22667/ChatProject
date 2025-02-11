using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class testTableEditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedMessages_Chats_ChatId",
                table: "Archived");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedMessages_User_UserId",
                table: "Archived");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_User_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "Archived");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualExpiration",
                table: "Chats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Chats",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Archived",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedMessages_Chats_ChatId",
                table: "Archived",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedMessages_User_UserId",
                table: "Archived",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_User_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_User_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedMessages_Chats_ChatId",
                table: "Archived");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivedMessages_User_UserId",
                table: "Archived");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_User_UserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_User_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ActualExpiration",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Archived",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "Archived",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedMessages_Chats_ChatId",
                table: "Archived",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivedMessages_User_UserId",
                table: "Archived",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_User_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
