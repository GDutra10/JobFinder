using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class UserToken_CustomerAndCompanyNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Companies_IdCompany",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Customers_IdCustomer",
                table: "UserTokens");

            migrationBuilder.AlterColumn<int>(
                name: "IdCustomer",
                table: "UserTokens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdCompany",
                table: "UserTokens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Companies_IdCompany",
                table: "UserTokens",
                column: "IdCompany",
                principalTable: "Companies",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Customers_IdCustomer",
                table: "UserTokens",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Companies_IdCompany",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Customers_IdCustomer",
                table: "UserTokens");

            migrationBuilder.AlterColumn<int>(
                name: "IdCustomer",
                table: "UserTokens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCompany",
                table: "UserTokens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Companies_IdCompany",
                table: "UserTokens",
                column: "IdCompany",
                principalTable: "Companies",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Customers_IdCustomer",
                table: "UserTokens",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
