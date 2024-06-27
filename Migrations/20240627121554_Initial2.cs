using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodefirst.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "adresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_adresses_PersonId",
                table: "adresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_PersonId",
                table: "Email",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_adresses_persons_PersonId",
                table: "adresses",
                column: "PersonId",
                principalTable: "persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adresses_persons_PersonId",
                table: "adresses");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropIndex(
                name: "IX_adresses_PersonId",
                table: "adresses");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "adresses");
        }
    }
}
