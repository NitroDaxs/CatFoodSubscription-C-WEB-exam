using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class RevertingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionProductsOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionProductsOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the order"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the product"),
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
    }
}
