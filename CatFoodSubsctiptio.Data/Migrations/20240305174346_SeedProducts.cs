using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Accessory" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsSubscription", "Name", "Price" },
                values: new object[] { 12, 4, "A multi-level cat tree designed to keep your feline friend entertained and comfortable.", "https://i.ibb.co/Jy1fQrV/Cat-Tree.png", false, "Cat Tree", 25.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsSubscription", "Name", "Price" },
                values: new object[] { 14, 4, "An interactive fishing rod toy that will engage your cat in playful antics for hours.", "https://i.ibb.co/tm04yZf/Fishing-Rod-Toy.png", false, "Fishing Rod Toy", 2.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsSubscription", "Name", "Price" },
                values: new object[] { 13, 5, "A stylish food bowl perfect for serving your cat's favorite wet and dry food.", "https://i.ibb.co/yy4md2H/Food-Bowl.png", false, "Food Bowl", 5.45m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
