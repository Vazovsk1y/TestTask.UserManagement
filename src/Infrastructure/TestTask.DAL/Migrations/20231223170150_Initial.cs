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
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), "Support" },
                    { new Guid("a97fb48b-c9c4-46c1-a8f7-3e7df678010d"), "SuperAdmin" },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), "Admin" },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FullName", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("0aee01ff-fd94-4180-9f37-ad841653e254"), 18, "ethan.silver@example.com", "Ethan Silver", "$2a$11$lRTmOLZAAnNvjAVVtPgdTuOlYE0TwHdgGvtVurhomUQGKdF1aBiOO" },
                    { new Guid("12a3c182-9a87-43ba-9f4e-547c71bda8fb"), 27, "emily.white.iii@example.com", "Emily White III", "$2a$11$TigpovzV0HRcD3eUbwu4i.yBNo2X56MwhIJTAcQbgXFGCeeldpCSm" },
                    { new Guid("15088f35-6457-4f2f-a5ca-e52db5c57f34"), 28, "john.doe.jr@example.com", "John Doe Jr.", "$2a$11$ivjCOanPT2eN5bz.KfbNMuFvzFuZBAPv9Yso1k1uPNQv7F4MXx.gO" },
                    { new Guid("18218134-b959-4f8b-8835-46d1d1ebb823"), 33, "daniel.brown.iii@example.com", "Daniel Brown III", "$2a$11$RQkEznGFpb5zsJU9PHc9FupSWkBD5v9zDTybkUrZBDPEExgIc5Y0S" },
                    { new Guid("218705ea-7161-4485-8bfb-978abc280ac6"), 16, "penis.vonyaet@gmail.com", "Mike Popchinskiy", "$2a$11$DucitYFpc7fvJT7KwGXXj.tXA8n5VvZM/7UQUqAgWqbm1G4GKAgi6" },
                    { new Guid("33ae8884-ac1a-450e-b4fc-2b7f9c881c57"), 30, "john.doe@example.com", "John Doe", "$2a$11$6rOm1UZomBv0S4j5KPVqbeKQnOa/8vMIPKicg/eQJn5gGtekFyHbC" },
                    { new Guid("47130bc1-1da1-4ff5-bb2e-14b445288c7f"), 29, "emma.green@example.com", "Emma Green", "$2a$11$TUh9ZJX26d7jZ.OLqUWUuuhUSP4Anc7HWutQV6/vJ5WFvtRP6VACy" },
                    { new Guid("4a71158c-da2e-45ad-a551-102c9bf7f138"), 28, "emily.white@example.com", "Emily White", "$2a$11$TxtRIB2XCnC5YzHnLgA1feeX2FbJTgDqWcKMxM.EioOwSOAps64Pu" },
                    { new Guid("67300aa7-f59e-4dc3-9d0d-24d5c999fec2"), 55, "john.doe.sr@example.com", "John Doe Sr.", "$2a$11$xivxWkkAztAyB8AdS5E8NOOFXjueQXdeYnMmwb8KxpIqeeL5gAd/q" },
                    { new Guid("7b4898ce-0b12-493e-9a5d-4c5685ee7070"), 25, "penis@gmail.com", "Mike Vazovskiy", "$2a$11$dP.CERPy/dTWDRvwVZlGIOy9EVTyD.XmnbcMcUp4NF8MbG4QokOvu" },
                    { new Guid("7c9a7c01-92a1-4aba-a792-eb99454a7999"), 45, "bob.smith@example.com", "Bob Smith", "$2a$11$bMivIJrhs/xs/gl.jNbnJuTIOe1TlxVh5Ir9Ast6f6PMnYQ7Gi/bi" },
                    { new Guid("82e6497b-8773-436b-a65b-46e4efab62ec"), 22, "alice.johnson@example.com", "Alice Johnson", "$2a$11$x/ZeLtIbjMzoZn4IlT0AYeBOvP59MPZa95BvROjFgsFhs4rwS/4qG" },
                    { new Guid("9541b57e-38c0-4443-99b4-9d482e76342d"), 40, "michael.grey@example.com", "Michael Grey", "$2a$11$NzyMYQRlgYsrHflWruN2xOFIi47VeP63X1nZeoUdHdalgFfM9H3/u" },
                    { new Guid("a7225293-3ab5-4d7f-9b9d-37e0dd9be8ff"), 14, "james.orange@example.com", "James Orange", "$2a$11$gAhK0NXWxOBT.Q/mnvw9L.Dngv7aEwSHLAuaXLi8F7zskswg1zrgK" },
                    { new Guid("ad2ce995-fee4-44f2-9255-067576d960c9"), 29, "daniel.brown.jr@example.com", "Daniel Brown Jr.", "$2a$11$twY5Cj3I3ZHoNFzPMeWLC.CWnZ82f8/68kX6wCkB4kyS.KScY9KWu" },
                    { new Guid("af7a643f-aef1-4dcf-938e-785ea8cb3cfe"), 31, "jack.miller@example.com", "Jack Miller", "$2a$11$oVR5hXPuY1W4cjmjWVz6Qu07gRmPKlY7MvpsxOpxqJZjoTbf2artu" },
                    { new Guid("b594fb48-c68d-4edf-a542-eeb9331f24c4"), 28, "jopka@gmail.com", "Sally Vazovskiy", "$2a$11$lFT.q313wcB8ZJK0fya3D.Va1bJGkQupr9jrZlk.7Z/5Y6Xy.gC.G" },
                    { new Guid("c9e02761-3237-4fff-8000-c849f4eb1efe"), 24, "olivia.red@example.com", "Olivia Red", "$2a$11$1LDi/OAEdYzf.bSpB0kwy.3UMG8Qzv06uxlP5sYp2w2Ldhi9SYvze" },
                    { new Guid("cbf6657f-1bb7-43ec-8740-5e66615a9dc8"), 32, "emily.white.ii@example.com", "Emily White II", "$2a$11$4eVQeWLLA2mtkePUDPemrefgHYNxJbg4qg8EM8o7Ojempm3B6ulWq" },
                    { new Guid("cf59211b-8078-42fc-b61c-6333c78a6e04"), 33, "william.yellow@example.com", "William Yellow", "$2a$11$U8p0Ixym6uEKlOnQ72PeV.74UEq1WINpsUWzeUaAVIr6YWmUgGzqK" },
                    { new Guid("d2070028-8290-4fe0-aff2-96872a3a2fa6"), 25, "mia.pink@example.com", "Mia Pink", "$2a$11$2xZt1JnVPL4BciLGQWmNGOgKGXOUGbPTZ8D5h6lXqm87b6JmhU5MK" },
                    { new Guid("d26e3b9e-11e5-4d28-84ec-102f1f4158e5"), 32, "alice.johnson.iii@example.com", "Alice Johnson III", "$2a$11$mOer3S3gL4c.ZigoH4T07e5/urJlWpj4DO43wWriYxf8Rp8VIFtW2" },
                    { new Guid("d291cc71-ed41-4dac-87b7-41054c7af4e6"), 26, "sophia.black@example.com", "Sophia Black", "$2a$11$an2t0GagZV1FF10MdaVvXe6Y1U8BbuXXELEe6gXZ6ph/q1iIZVP7u" },
                    { new Guid("d5c12da2-dcc5-4aa8-8b80-068ff4073d9e"), 35, "bob.smith.iii@example.com", "Bob Smith III", "$2a$11$EshUVTkqnbNLKN0aMKRaF.zkcW78QfUTJW3AYnEw1FWcBNvTIqZwa" },
                    { new Guid("ed6c2ac1-d517-44a4-9b4e-248ce11b0ec7"), 27, "ava.purple@example.com", "Ava Purple", "$2a$11$gI9.skIi52qzhJWNjDMQVuwT6RFH7wBRH8f0VzDgfez7IPTRPANXy" },
                    { new Guid("f0e4bf66-6b73-4b7e-9eb7-4a09def821d0"), 25, "alice.johnson.ii@example.com", "Alice Johnson II", "$2a$11$irB6ifkUMhA0BdIWnl4Lb.EqAR2jnn8v8CmI7kNmRKQdwfZ0.MHpa" },
                    { new Guid("f3594cfc-2959-4197-a0d3-64dfd4f1f26c"), 30, "bob.smith.jr@example.com", "Bob Smith Jr.", "$2a$11$QrW0ew1qrjfbsosr5qMTqeGqkOEE6.olWnQTLAtQkURlFCQRBDXTO" },
                    { new Guid("f370d916-2219-4cc2-acd8-8eddff93f509"), 34, "govno@gmail.com", "Ivan Vazovskiy", "$2a$11$jbYCelEnNnwzS4SI60llCeNAE4XVmO3PN9HtBZe4S.j687uYmYZgC" },
                    { new Guid("fba5aaee-f86d-47e4-a1b1-dc19be451356"), 19, "kurwa.vonyaet@gmail.com", "Mike Kurwa", "$2a$11$VVJ7tprqDiqytykEnGEZ8u1alJUG4nfERAHpFA743pOykTct4CL0m" },
                    { new Guid("feaaa763-f3f6-4a68-ba40-a2ea556d8e44"), 35, "daniel.brown@example.com", "Daniel Brown", "$2a$11$edyzWH1aUyUDF5J8ZepF5OlOTDJP8x0RxnoOKIXKcnfIIha.PO8Oq" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("0aee01ff-fd94-4180-9f37-ad841653e254") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("12a3c182-9a87-43ba-9f4e-547c71bda8fb") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("15088f35-6457-4f2f-a5ca-e52db5c57f34") },
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), new Guid("18218134-b959-4f8b-8835-46d1d1ebb823") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("18218134-b959-4f8b-8835-46d1d1ebb823") },
                    { new Guid("a97fb48b-c9c4-46c1-a8f7-3e7df678010d"), new Guid("218705ea-7161-4485-8bfb-978abc280ac6") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("218705ea-7161-4485-8bfb-978abc280ac6") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("218705ea-7161-4485-8bfb-978abc280ac6") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("33ae8884-ac1a-450e-b4fc-2b7f9c881c57") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("47130bc1-1da1-4ff5-bb2e-14b445288c7f") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("47130bc1-1da1-4ff5-bb2e-14b445288c7f") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("4a71158c-da2e-45ad-a551-102c9bf7f138") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("67300aa7-f59e-4dc3-9d0d-24d5c999fec2") },
                    { new Guid("a97fb48b-c9c4-46c1-a8f7-3e7df678010d"), new Guid("7b4898ce-0b12-493e-9a5d-4c5685ee7070") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("7b4898ce-0b12-493e-9a5d-4c5685ee7070") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("7b4898ce-0b12-493e-9a5d-4c5685ee7070") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("7c9a7c01-92a1-4aba-a792-eb99454a7999") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("7c9a7c01-92a1-4aba-a792-eb99454a7999") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("82e6497b-8773-436b-a65b-46e4efab62ec") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("9541b57e-38c0-4443-99b4-9d482e76342d") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("a7225293-3ab5-4d7f-9b9d-37e0dd9be8ff") },
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), new Guid("ad2ce995-fee4-44f2-9255-067576d960c9") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("ad2ce995-fee4-44f2-9255-067576d960c9") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("af7a643f-aef1-4dcf-938e-785ea8cb3cfe") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("b594fb48-c68d-4edf-a542-eeb9331f24c4") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("b594fb48-c68d-4edf-a542-eeb9331f24c4") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("c9e02761-3237-4fff-8000-c849f4eb1efe") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("cbf6657f-1bb7-43ec-8740-5e66615a9dc8") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("cf59211b-8078-42fc-b61c-6333c78a6e04") },
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), new Guid("d2070028-8290-4fe0-aff2-96872a3a2fa6") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("d2070028-8290-4fe0-aff2-96872a3a2fa6") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("d26e3b9e-11e5-4d28-84ec-102f1f4158e5") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("d291cc71-ed41-4dac-87b7-41054c7af4e6") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("d5c12da2-dcc5-4aa8-8b80-068ff4073d9e") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("d5c12da2-dcc5-4aa8-8b80-068ff4073d9e") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("ed6c2ac1-d517-44a4-9b4e-248ce11b0ec7") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("ed6c2ac1-d517-44a4-9b4e-248ce11b0ec7") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("f0e4bf66-6b73-4b7e-9eb7-4a09def821d0") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("f3594cfc-2959-4197-a0d3-64dfd4f1f26c") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("f3594cfc-2959-4197-a0d3-64dfd4f1f26c") },
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), new Guid("f370d916-2219-4cc2-acd8-8eddff93f509") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("f370d916-2219-4cc2-acd8-8eddff93f509") },
                    { new Guid("a97fb48b-c9c4-46c1-a8f7-3e7df678010d"), new Guid("fba5aaee-f86d-47e4-a1b1-dc19be451356") },
                    { new Guid("d92227c9-43b5-413b-976f-aeed7c374ae8"), new Guid("fba5aaee-f86d-47e4-a1b1-dc19be451356") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("fba5aaee-f86d-47e4-a1b1-dc19be451356") },
                    { new Guid("20253c25-6a04-457c-bab8-bf3c124820b1"), new Guid("feaaa763-f3f6-4a68-ba40-a2ea556d8e44") },
                    { new Guid("edbfdba1-54d9-4c9e-9f26-f85037648d70"), new Guid("feaaa763-f3f6-4a68-ba40-a2ea556d8e44") }
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
