using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class ProductUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad196c19-e7fb-4b31-958e-ab8f773fcac6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de59a9c8-6631-4338-9349-9a433d754259");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the product is deleted");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20c48bc6-4a7f-4114-b014-b75f9dd0a4d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "878a3e7a-6e62-454b-ab45-9251c5818c6f");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "ad196c19-e7fb-4b31-958e-ab8f773fcac6", "3907fc74-c79e-42e4-9766-e48bcfca00aa", "IdentityRole", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "de59a9c8-6631-4338-9349-9a433d754259", "029a8e5f-3766-4777-b752-a2b5aa2205f8", "IdentityRole", "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEK4u9YX8Roz41b+ZXfC4qfN+OX4/XLL86L9qvBzIyiOZOm8K7MgXK6tg2PY5A+goiA==");
        }
    }
}
