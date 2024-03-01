using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class OrderAndProductChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the product is a subscription");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Quantity of the product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the order is a subscription");
        }
    }
}
