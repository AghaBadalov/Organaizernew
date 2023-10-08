using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organaizer.Migrations
{
    public partial class Orgimagesupdated6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganaizerId",
                table: "OrganaizerImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganaizerId",
                table: "OrganaizerImages",
                type: "int",
                nullable: true);
        }
    }
}
