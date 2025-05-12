using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstoreapp.Migrations
{
    /// <inheritdoc />
    public partial class changeupdatecode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Books",
                schema: "tridente_kamini",
                newName: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tridente_kamini");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Books",
                newSchema: "tridente_kamini");
        }
    }
}
