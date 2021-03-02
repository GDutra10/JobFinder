using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobFinder.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NuTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    NmUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NuTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Customers_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    IdJob = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VlSalaryMin = table.Column<float>(type: "real", nullable: true),
                    VlSalaryMax = table.Column<float>(type: "real", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.IdJob);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Companies",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    IdCandidate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdJob = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    WasAccept = table.Column<bool>(type: "bit", nullable: false),
                    WasReject = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.IdCandidate);
                    table.ForeignKey(
                        name: "FK_Candidates_Customers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Customers",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidates_Jobs_IdJob",
                        column: x => x.IdJob,
                        principalTable: "Jobs",
                        principalColumn: "IdJob",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_IdJob",
                table: "Candidates",
                column: "IdJob");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_IdUser",
                table: "Candidates",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdRole",
                table: "Customers",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IdRole",
                table: "Jobs",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_IdUser",
                table: "Jobs",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
