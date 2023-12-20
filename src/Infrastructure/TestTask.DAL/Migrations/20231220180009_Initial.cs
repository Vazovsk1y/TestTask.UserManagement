using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd"), "Admin" },
                    { new Guid("1e0940e1-6e7b-4786-9f73-0b1fce52f8ee"), "SuperAdmin" },
                    { new Guid("9c6f35a3-6be9-4e0b-a3a0-6c7c50db6464"), "Support" },
                    { new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a"), 34, "govno@gmail.com", "Ivan Vazovskiy", "$2a$11$YYjEojDUW.dbGb9R/QSE4O6pmTsW7ZtzwYSy.7DGt0VfMHrFgLMWC" },
                    { new Guid("c6f7040b-c383-49cd-9336-11118eaf384d"), 28, "jopka@gmail.com", "Sally Vazovskiy", "$2a$11$S.qmXp9l/sfcE3uF4XlWlO74uIbZuSVlColXBwD9peuAydSR40UYC" },
                    { new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3"), 25, "penis@gmail.com", "Mike Vazovskiy", "$2a$11$8exyJAoj3b1oqKn4CBdd4upnwp5FCVDBTM/BpY9XickzyPXh8u61u" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("9c6f35a3-6be9-4e0b-a3a0-6c7c50db6464"), new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a") },
                    { new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922"), new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a") },
                    { new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd"), new Guid("c6f7040b-c383-49cd-9336-11118eaf384d") },
                    { new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922"), new Guid("c6f7040b-c383-49cd-9336-11118eaf384d") },
                    { new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd"), new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3") },
                    { new Guid("1e0940e1-6e7b-4786-9f73-0b1fce52f8ee"), new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3") },
                    { new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922"), new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Title",
                table: "Roles",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
