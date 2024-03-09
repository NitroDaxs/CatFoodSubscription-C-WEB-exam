using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class AddressChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Addresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "First name of the customer");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Addresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Last name of the customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "First name of the customer");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Last name of the customer");
        }
    }
}
