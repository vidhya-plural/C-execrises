using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberstest4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Biography",
                table: "Authors",
                newName: "biography");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Authors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "biography",
                table: "Authors",
                newName: "Biography");
        }
    }
}
