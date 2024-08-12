using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberstest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1,
                column: "image",
                value: "harry-potter.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 2,
                column: "image",
                value: "game-of-thrones.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Books");
        }
    }
}
