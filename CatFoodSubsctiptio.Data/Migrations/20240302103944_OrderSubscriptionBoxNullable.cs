using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class OrderSubscriptionBoxNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SubscriptionBoxes_SubscriptionBoxId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionBoxId",
                table: "Orders",
                type: "int",
                nullable: true,
                comment: "Identification of the subscriptionBox",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Identification of the subscriptionBox");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SubscriptionBoxes_SubscriptionBoxId",
                table: "Orders",
                column: "SubscriptionBoxId",
                principalTable: "SubscriptionBoxes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SubscriptionBoxes_SubscriptionBoxId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionBoxId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Identification of the subscriptionBox",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Identification of the subscriptionBox");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SubscriptionBoxes_SubscriptionBoxId",
                table: "Orders",
                column: "SubscriptionBoxId",
                principalTable: "SubscriptionBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
