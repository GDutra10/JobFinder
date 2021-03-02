using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class AddTokenInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeToken",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeToken",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeToken",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeToken",
                table: "Companies");
        }
    }
}
