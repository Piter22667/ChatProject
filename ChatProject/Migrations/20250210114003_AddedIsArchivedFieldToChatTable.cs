using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsArchivedFieldToChatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualExpiration",
                table: "Chats");

            migrationBuilder.AddColumn<bool>(
                name: "isArchived",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isArchived",
                table: "Chats");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualExpiration",
                table: "Chats",
                type: "datetime2",
                nullable: true);
        }
    }
}
