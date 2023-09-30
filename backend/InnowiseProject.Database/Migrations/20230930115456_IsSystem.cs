using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseProject.Database.Migrations
{
    /// <inheritdoc />
    public partial class IsSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystem",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystem",
                table: "AspNetUsers");
        }
    }
}
