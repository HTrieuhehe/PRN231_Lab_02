using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODataBookStore.Migrations
{
    public partial class BookStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.City);
                });

            migrationBuilder.CreateTable(
                name: "Presses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Address_LocationName",
                        column: x => x.LocationName,
                        principalTable: "Address",
                        principalColumn: "City",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Presses_PressId",
                        column: x => x.PressId,
                        principalTable: "Presses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "City", "Street" },
                values: new object[,]
                {
                    { "Da Nang City", "D1, Thu Duc District" },
                    { "Ha Noi City", "D3, Thu Duc District" },
                    { "HCM City", "D2, Thu Duc District" },
                    { "Quy Nhon City", "D6, Thu Duc District" }
                });

            migrationBuilder.InsertData(
                table: "Presses",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Addison-Wesley" },
                    { 2, 1, "Addison-Mercedes" },
                    { 3, 2, "John-Doe" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Account" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "LocationName", "PressId", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Mark Michaelis", "978-0-321-87758-1", "HCM City", 1, 59.99m, "Essential C#5.0" },
                    { 2, "Mark Wiens", "123-0-321-87758-1", "Ha Noi City", 2, 49.99m, "Essential C#6.0" },
                    { 3, "Michelin", "234-0-321-87758-1", "Da Nang City", 2, 33.99m, "Food Blog" },
                    { 4, "Steve Krug", "345-0-321-87758-1", "Quy Nhon City", 3, 159.99m, "Don't Make Me Think" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LocationName",
                table: "Books",
                column: "LocationName");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PressId",
                table: "Books",
                column: "PressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Presses");
        }
    }
}
