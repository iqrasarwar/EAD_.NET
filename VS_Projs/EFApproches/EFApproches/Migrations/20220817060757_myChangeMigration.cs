using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFApproches.Migrations
{
    /// <inheritdoc />
    public partial class myChangeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Testa",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Testa");
        }
    }
}
