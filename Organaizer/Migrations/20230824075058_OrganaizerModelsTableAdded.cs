using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organaizer.Migrations
{
    public partial class OrganaizerModelsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organaizers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(110)", maxLength: 110, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organaizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganaizerImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganaizerId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPoster = table.Column<bool>(type: "bit", nullable: false),
                    OrganaizerModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganaizerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganaizerImages_Organaizers_OrganaizerModelId",
                        column: x => x.OrganaizerModelId,
                        principalTable: "Organaizers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganaizerImages_OrganaizerModelId",
                table: "OrganaizerImages",
                column: "OrganaizerModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganaizerImages");

            migrationBuilder.DropTable(
                name: "Organaizers");
        }
    }
}
