using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1,
                column: "image",
                value: "data:image/jpg;base64,<Base64ImageString>");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 2,
                column: "image",
                value: "data:image/jpg;base64,<Base64ImageString>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
