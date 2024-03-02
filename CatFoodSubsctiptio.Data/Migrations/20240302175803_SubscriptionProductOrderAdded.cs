using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class SubscriptionProductOrderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscription",
                table: "ProductsOrders");

            migrationBuilder.CreateTable(
                name: "SubscriptionProductsOrders",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the product"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the order"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of the product")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionProductsOrders", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SubscriptionProductsOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionProductsOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionProductsOrders_ProductId",
                table: "SubscriptionProductsOrders",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionProductsOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscription",
                table: "ProductsOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicates whether the product is a subscription");
        }
    }
}
