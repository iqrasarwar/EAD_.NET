using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFApproches.Migrations
{
    /// <inheritdoc />
    public partial class mtMigration : Migration
    {
        //comment code of this
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.CreateTable(
                name: "Testa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testa", x => x.Id);
                });*/
        }

        //delete this tabel
       /* /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Testa");
        }*/
    }
}
