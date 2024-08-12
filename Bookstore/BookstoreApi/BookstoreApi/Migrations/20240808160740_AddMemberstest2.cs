using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberstest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "genre_name");

            migrationBuilder.RenameColumn(
                name: "genre_id",
                table: "Genres",
                newName: "genre_id");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "genre_name",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "genre_id",
                table: "Genres",
                newName: "genre_id");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
