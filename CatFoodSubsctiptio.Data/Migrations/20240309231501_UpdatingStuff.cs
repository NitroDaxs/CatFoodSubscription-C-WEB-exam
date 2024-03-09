using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class UpdatingStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05322696-75e8-421a-ac33-f2fccb9de9bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56ad20e9-5009-4d23-b3d2-af8fcc3c2057");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "7fdd7ad5-d514-4c65-b694-701efef64472", "f3828f51-89f7-47ad-940a-15fbe21a9377", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "cb78c4b9-38ad-4f91-ae98-dc7b504870c2", "b25d9063-ffc5-43bb-8081-f5c9b765fe1b", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGbHi/jzbU75vzCwv5OmmNwFZd19EQw0sRP1BDJur9DPkUJkeTlyYFgSPc7ZRaY8Ig==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fdd7ad5-d514-4c65-b694-701efef64472");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb78c4b9-38ad-4f91-ae98-dc7b504870c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "05322696-75e8-421a-ac33-f2fccb9de9bd", "7f5a8af9-49d1-44c2-87a0-18896b434476", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "56ad20e9-5009-4d23-b3d2-af8fcc3c2057", "9cc15d02-7a8f-4f59-971d-ee9bfa5b73ed", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEDr920i7GsWS4S36nQskncXI8iima0LNGPHIwnrZ62E/AfJKkcLE/FUTCEBfc+9VlA==");
        }
    }
}
