using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class AddUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeToken",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeToken",
                table: "Companies");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    IdUserToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.IdUserToken);
                    table.ForeignKey(
                        name: "FK_UserTokens_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTokens_Customers_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customers",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_IdCompany",
                table: "UserTokens",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_IdCustomer",
                table: "UserTokens",
                column: "IdCustomer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTokens");

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
    }
}
