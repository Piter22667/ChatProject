using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedArchiveProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                 CREATE PROCEDURE ArchiveExpiredChats
                 AS
                 BEGIN
                     INSERT INTO Archived(ChatId, UserId, Content, SentTime)
                     SELECT m.ChatId, m.UserId, m.Content, m.SentTime
                     FROM Messages m
                     INNER JOIN Chats c ON m.ChatId = c.Id
                     WHERE c.Expiration <= GETUTCDATE() OR (c.Expiration IS NULL AND DATEADD(DAY, 14, c.CreationTime) <= GETUTCDATE());

                     DELETE FROM Messages
                     WHERE ChatId IN (SELECT Id FROM Chats WHERE Expiration <= GETUTCDATE() OR (Expiration IS NULL AND DATEADD(DAY, 14, CreationTime) <= GETUTCDATE()));

                     UPDATE Chats
                     SET isArchived = 1
                     WHERE Expiration <= GETUTCDATE() OR (Expiration IS NULL AND DATEADD(DAY, 14, CreationTime) <= GETUTCDATE());
                 END;
             ";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"DROP PROCEDURE IF EXISTS ArchiveExpiredChats;";
            migrationBuilder.Sql(sql);
        }
    }
}
