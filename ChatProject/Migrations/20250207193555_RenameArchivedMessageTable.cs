using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatProject.Migrations
{
    /// <inheritdoc />
    public partial class RenameArchivedMessageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   //         migrationBuilder.RenameTable(
   //             name: "ArchivedMessages",
   //             newName: "Archived"
   //);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
  //          migrationBuilder.RenameTable(
  //            name: "Archived",
  //            newName: "ArchivedMessages"
  //);
        }
    }
}
