using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e73559d-a7da-48f6-9cf2-03a1d1a8373f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4baa24fb-ca4a-41eb-9d57-81659a3d90e3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "444f23c3-6d00-4eb6-8abe-7e3f9871336c", "57b5f2b4-780a-436d-a2ec-4deceff426c6", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "67e67d83-fd78-429d-a18f-d23a08b586a1", "83921415-4b08-4549-97ab-fd3bdd195fa4", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b", 0, "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b", "admin@gmail.com", true, false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEPY2suf0v77pkhg2VE/jdmVfZMXJltv/eUTPgMaYn6hKdazTtMTAIdF+hX0P8UlcgQ==", null, false, "ADMIN@GMAIL.COM", false, "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "444f23c3-6d00-4eb6-8abe-7e3f9871336c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e67d83-fd78-429d-a18f-d23a08b586a1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0e73559d-a7da-48f6-9cf2-03a1d1a8373f", "9aaebb8a-9653-4def-b8a0-2f12ebecd8ea", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "4baa24fb-ca4a-41eb-9d57-81659a3d90e3", "f1b95222-dfda-40f6-a046-ce1ad955f8b4", "IdentityRole", "Admin", "ADMIN" });
        }
    }
}
