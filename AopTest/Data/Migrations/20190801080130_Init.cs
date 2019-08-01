using Microsoft.EntityFrameworkCore.Migrations;

namespace AopTest.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, "Crash", "Crash" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 2, "Sol", "Sol" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 3, "Test", "Test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
