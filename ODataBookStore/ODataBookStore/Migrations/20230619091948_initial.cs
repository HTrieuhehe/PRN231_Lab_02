using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODataBookStore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Role", x => x.Id);
                 });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varbinary(max)", nullable: false)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_User", x => x.Id);
                     table.ForeignKey(
                         name: "FK_User_Role",
                         column: x => x.RoleId,
                         principalTable: "Role",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                 });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varbinary(max)", nullable: false)
                },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Account", x => x.Id);
                     table.ForeignKey(
                         name: "FK_Account_Role",
                         column: x => x.RoleId,
                         principalTable: "Role",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
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
                name: "Press",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Press", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
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
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Address_LocationName",
                        column: x => x.LocationName,
                        principalTable: "Address",
                        principalColumn: "City",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Press_PressId",
                        column: x => x.PressId,
                        principalTable: "Press",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "Id", "RoleName"},
            //    values: new object[,]
            //    {
            //        { 1, "Account"},
            //        { 2, "User" }
            //    });

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
                table: "Press",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Addison-Wesley" },
                    { 2, 1, "Addison-Mercedes" },
                    { 3, 2, "John-Doe" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "ISBN", "LocationName", "PressId", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Mark Michaelis", "978-0-321-87758-1", "HCM City", 1, 59.99m, "Essential C#5.0" },
                    { 2, "Mark Wiens", "123-0-321-87758-1", "Ha Noi City", 2, 49.99m, "Essential C#6.0" },
                    { 3, "Michelin", "234-0-321-87758-1", "Da Nang City", 2, 33.99m, "Food Blog" },
                    { 4, "Steve Krug", "345-0-321-87758-1", "Quy Nhon City", 3, 159.99m, "Don't Make Me Think" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_LocationName",
                table: "Book",
                column: "LocationName");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PressId",
                table: "Book",
                column: "PressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "User");
            
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Press");
        }


        //mở Package Manager Console lên trỏ tới cái project API và nhập update-database là ok

    }
}
