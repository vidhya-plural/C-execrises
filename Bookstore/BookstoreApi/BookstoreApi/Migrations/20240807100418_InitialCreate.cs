using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "author_id", "Biography", "Name" },
                values: new object[,]
                {
                    { 1, "British author, best known for the Harry Potter series.", "J.K. Rowling" },
                    { 2, "American novelist and short story writer, known for A Song of Ice and Fire.", "George R.R. Martin" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genre_id", "GenreName" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Science Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "book_id", "author_id", "genre_id", "price", "publication_date", "title" },
                values: new object[,]
                {
                    { 1, 1, 1, 19.99m, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Sorcerer's Stone" },
                    { 2, 2, 1, 15.99m, new DateTime(1996, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Game of Thrones" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "genre_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "author_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "author_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "genre_id",
                keyValue: 1);
        }
    }
}
