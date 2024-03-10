using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class ChangingPostCOde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9f3b538-1185-4eca-b398-b9cca1278fa4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca38621d-29a6-41da-b94e-42a3e9974f66");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                comment: "PostalCode of the address",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "PostalCode of the address");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ef0e9a4e-10f1-42cb-b8a0-5c239dc25c66", "e2ad2ddb-6aa3-4e0e-ba22-83b98abd346f", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "fd3fa4f5-ac49-48de-ba75-bfe43c46e481", "b77c6225-9037-41ab-b540-a4e81af84916", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEKUOwKwcvBO5OgwmpilXVWtmfoB2zzWgxdqZ7sDJkNKZNSBCdUSZg+ugXmChVH7fHQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef0e9a4e-10f1-42cb-b8a0-5c239dc25c66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd3fa4f5-ac49-48de-ba75-bfe43c46e481");

            migrationBuilder.AlterColumn<int>(
                name: "PostalCode",
                table: "Addresses",
                type: "int",
                nullable: false,
                comment: "PostalCode of the address",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "PostalCode of the address");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "c9f3b538-1185-4eca-b398-b9cca1278fa4", "d277403e-b3a9-493d-a336-b8bd1f84bb78", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ca38621d-29a6-41da-b94e-42a3e9974f66", "58dbbaeb-0a3e-48ec-b60a-1eca45c4851d", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEK0pQgxj5TAhi0FgqfpB2jxMZg6k5i6Wv28nASCKBuV8N1u2/MzIcndH6xVaMnEUag==");
        }
    }
}
