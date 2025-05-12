using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstoreapp.Migrations
{
    /// <inheritdoc />
    public partial class chnagedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCode",
                schema: "tridente_kamini",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookCode",
                schema: "tridente_kamini",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
