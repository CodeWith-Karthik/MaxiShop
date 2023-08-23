using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateOn",
                table: "Category",
                newName: "CreatedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Category",
                newName: "CreateOn");
        }
    }
}
