using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatFileMapTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatFileNameMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatFileNameMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatFileNameMap_ChatFile_FileId",
                        column: x => x.FileId,
                        principalTable: "ChatFile",
                        principalColumn: "stream_id");
                    table.ForeignKey(
                        name: "FK_ChatFileNameMap_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileNameMap_FileId",
                table: "ChatFileNameMap",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatFileNameMap_MessageId",
                table: "ChatFileNameMap",
                column: "MessageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatFileNameMap");
        }
    }
}
