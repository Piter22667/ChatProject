using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedFilestramClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TABLE dbo.ChatFile AS FileTable
            WITH (
                FILETABLE_DIRECTORY = 'ChatFile', 
                FILETABLE_COLLATE_FILENAME = database_default
    );");

            migrationBuilder.CreateTable(
                name: "ChatFileConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFileConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatFileConnections_ChatFile_FileId",
                        column: x => x.FileId,
                        principalTable: "ChatFile",
                        principalColumn: "stream_id");
                    table.ForeignKey(
                        name: "FK_ChatFileConnections_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileConnections_ChatId",
                table: "ChatFileConnections",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileConnections_FileId",
                table: "ChatFileConnections",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatFileConnections");

            migrationBuilder.DropTable(
                name: "ChatFile");
        }
    }
}
