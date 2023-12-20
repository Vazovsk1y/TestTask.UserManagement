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
                    { new Guid("10b2330b-89b1-4262-b9f3-9963d47e22e6"), "Support" },
                    { new Guid("2771ba05-86d7-4fb3-9884-cf60755e56c9"), "User" },
                    { new Guid("53bd3d14-5c0c-4c0c-b45f-365865b09804"), "Admin" },
                    { new Guid("ade6e94e-73fb-4cee-9a93-ec008be89748"), "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("df27527b-7158-47e0-b55e-0d075f751170"), 25, "penis@gmail.com", "Mike Vazovskiy", "$2a$11$N.2UL/6z7EeOqU4H4CjewOV6pU6rLT9n4LQk0BkgkWmbZ79SM5LF." },
                    { new Guid("e56cfd2f-6619-420d-8d9a-3bcd5f8ef4e1"), 34, "govno@gmail.com", "Иван Сидоров", "$2a$11$lxUKKVd01uxUvyeVLVQHXuK7Yj5kzH6cjhY5uh0aE.CuFiEpRQVIO" },
                    { new Guid("e5e3008f-93bb-4d44-8f33-e37f2b5e69ac"), 28, "jopka@gmail.com", "Иван Иванов", "$2a$11$lWlWuU1qBurUrtyO2JeufusC.n0D1lQVyF9M5uZTBMB9cNO/tv.1a" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("14acbff4-2f9e-4d78-85f1-22fb875ec319"), new Guid("74156d72-d1e7-4b7c-a7d6-858e7893dc02") },
                    { new Guid("f700d78f-dfd1-4593-9a34-cf098296b53c"), new Guid("74156d72-d1e7-4b7c-a7d6-858e7893dc02") },
                    { new Guid("183e83b0-6027-4771-82c4-53a15de4bf66"), new Guid("cb101933-ddd2-41df-ad02-adacf58bebd5") },
                    { new Guid("5abeb509-a426-4dad-90fa-bb51243943cd"), new Guid("cb101933-ddd2-41df-ad02-adacf58bebd5") },
                    { new Guid("c4a51d31-7dd4-440a-846a-b45b63e4075f"), new Guid("cb101933-ddd2-41df-ad02-adacf58bebd5") },
                    { new Guid("8df2a3d9-d479-4c5a-a8f2-e26f3a77a772"), new Guid("fe8482e1-4123-4c7e-97e2-50ad4192b80b") },
                    { new Guid("cabafc03-fb8c-41b6-a23f-59378ef91cd9"), new Guid("fe8482e1-4123-4c7e-97e2-50ad4192b80b") }
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
