using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class ProductOrderIsSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "ProductsOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the product is a subscription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "ProductsOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the product is a subscription");
        }
    }
}
