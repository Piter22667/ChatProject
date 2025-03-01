using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFilesToConnectWithMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileConnections_Chats_ChatId",
                table: "ChatFileConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileNameMap_ChatFile_FileId",
                table: "ChatFileNameMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileNameMap_Messages_MessageId",
                table: "ChatFileNameMap");

            migrationBuilder.DropIndex(
                name: "IX_ChatFileNameMap_FileId",
                table: "ChatFileNameMap");

            migrationBuilder.DropIndex(
                name: "IX_ChatFileConnections_ChatId",
                table: "ChatFileConnections");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatFileConnections");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatFileStreamId",
                table: "ChatFileNameMap",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "ChatFileConnections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileNameMap_ChatFileStreamId",
                table: "ChatFileNameMap",
                column: "ChatFileStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileConnections_MessageId",
                table: "ChatFileConnections",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileConnections_Messages_MessageId",
                table: "ChatFileConnections",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileNameMap_ChatFile_ChatFileStreamId",
                table: "ChatFileNameMap",
                column: "ChatFileStreamId",
                principalTable: "ChatFile",
                principalColumn: "stream_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileNameMap_Messages_MessageId",
                table: "ChatFileNameMap",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileConnections_Messages_MessageId",
                table: "ChatFileConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileNameMap_ChatFile_ChatFileStreamId",
                table: "ChatFileNameMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatFileNameMap_Messages_MessageId",
                table: "ChatFileNameMap");

            migrationBuilder.DropIndex(
                name: "IX_ChatFileNameMap_ChatFileStreamId",
                table: "ChatFileNameMap");

            migrationBuilder.DropIndex(
                name: "IX_ChatFileConnections_MessageId",
                table: "ChatFileConnections");

            migrationBuilder.DropColumn(
                name: "ChatFileStreamId",
                table: "ChatFileNameMap");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "ChatFileConnections");

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "ChatFileConnections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileNameMap_FileId",
                table: "ChatFileNameMap",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileConnections_ChatId",
                table: "ChatFileConnections",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileConnections_Chats_ChatId",
                table: "ChatFileConnections",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileNameMap_ChatFile_FileId",
                table: "ChatFileNameMap",
                column: "FileId",
                principalTable: "ChatFile",
                principalColumn: "stream_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatFileNameMap_Messages_MessageId",
                table: "ChatFileNameMap",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}
