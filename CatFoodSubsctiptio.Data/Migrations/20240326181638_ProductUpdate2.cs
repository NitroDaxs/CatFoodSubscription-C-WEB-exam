using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class ProductUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c48bc6-4a7f-4114-b014-b75f9dd0a4d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "878a3e7a-6e62-454b-ab45-9251c5818c6f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "71f65851-1f23-459f-8b67-a6a00324fd39", "3b50f8ab-e84e-4133-a7c6-7f7cfcb1bd95", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8ac1f2a2-6638-4ef3-866d-b2b19c2ce92c", "e161d944-7465-492d-b94d-7a7813c0d39e", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAECCuf/NafX73DSFIGCjRVw1fXVOaavIOks2a2n7EBuvkXOboi+BCovd+QEOArUnGAw==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71f65851-1f23-459f-8b67-a6a00324fd39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ac1f2a2-6638-4ef3-866d-b2b19c2ce92c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "20c48bc6-4a7f-4114-b014-b75f9dd0a4d1", "1e916de3-c501-47fa-b331-6f357a89ccba", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "878a3e7a-6e62-454b-ab45-9251c5818c6f", "b28bd2c3-e124-44f7-b43e-82bdd52230c2", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEI64t2aC8WvxLBglAbbOXar5ITBi6E6shPkSOXlWujNM1DbG1As45T7yHEX8cMrjYg==");
        }
    }
}
