using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class Subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef0e9a4e-10f1-42cb-b8a0-5c239dc25c66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd3fa4f5-ac49-48de-ba75-bfe43c46e481");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscriptionCanceled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates if the subscription is canceled");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad196c19-e7fb-4b31-958e-ab8f773fcac6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de59a9c8-6631-4338-9349-9a433d754259");

            migrationBuilder.DropColumn(
                name: "IsSubscriptionCanceled",
                table: "Orders");

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
    }
}
