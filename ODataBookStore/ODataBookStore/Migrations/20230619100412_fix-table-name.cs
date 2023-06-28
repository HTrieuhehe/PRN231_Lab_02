using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODataBookStore.Migrations
{
    public partial class fixtablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Address_LocationName",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Press_PressId",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Press",
                table: "Press");

            #region Drop FK Authorize

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Role",
                table: "Account");

            #endregion

            #region Drop PK Authorize
            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");
            #endregion

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Press",
                newName: "Presses");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            #region Rename Authorize Table
            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");
            #endregion

            migrationBuilder.RenameIndex(
                name: "IX_Book_PressId",
                table: "Books",
                newName: "IX_Books_PressId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_LocationName",
                table: "Books",
                newName: "IX_Books_LocationName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presses",
                table: "Presses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            #region Add PK Authorize
            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");
            #endregion

            #region Add FK Authorize
            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Role",
                table: "Accounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            #endregion

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Address_LocationName",
                table: "Books",
                column: "LocationName",
                principalTable: "Address",
                principalColumn: "City",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Presses_PressId",
                table: "Books",
                column: "PressId",
                principalTable: "Presses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Address_LocationName",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Presses_PressId",
                table: "Books");

            #region Drop Authorize FK
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Role",
                table: "Accounts");
            #endregion

            #region Drop Authorize PK
            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");
            #endregion

            migrationBuilder.DropPrimaryKey(
                name: "PK_Presses",
                table: "Presses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            #region Rename Authorize Table
            migrationBuilder.RenameTable(
               name: "Roles",
               newName: "Role");

            migrationBuilder.RenameTable(
                name: "Acocunts",
                newName: "Account");

            migrationBuilder.RenameTable(
               name: "Users",
               newName: "User");
            #endregion

            migrationBuilder.RenameTable(
                name: "Presses",
                newName: "Press");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PressId",
                table: "Book",
                newName: "IX_Book_PressId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_LocationName",
                table: "Book",
                newName: "IX_Book_LocationName");

            #region Add Authorize PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "Press",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            #endregion

            migrationBuilder.AddPrimaryKey(
                name: "PK_Press",
                table: "Press",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            #region Add Authorize FK

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Acocunt_Role",
                table: "Account",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            #endregion

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Address_LocationName",
                table: "Book",
                column: "LocationName",
                principalTable: "Address",
                principalColumn: "City",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Press_PressId",
                table: "Book",
                column: "PressId",
                principalTable: "Press",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
