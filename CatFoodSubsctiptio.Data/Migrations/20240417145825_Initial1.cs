using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatFoodSubscription.Data.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identification for the address")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(56)", maxLength: 56, nullable: false, comment: "Country of the address"),
                    City = table.Column<string>(type: "nvarchar(58)", maxLength: 58, nullable: false, comment: "City of the address"),
                    Street = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Street of the address"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "PostalCode of the address"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "First name of the customer"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Last name of the customer"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "PhoneNumber for the address"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Email for the address")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Specifies if the customer account is deleted"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identification for the category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "Name of the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identification of the subscriptionBox")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the subscriptionBox"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Path for subscriptionBox image"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Name of the subscriptionBox"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the subscriptionBox")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identification for the product")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Name of the product"),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Description of the product"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Price of the product"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Path leading to the product's image"),
                    IsSubscription = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the product is subscription based"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Identification for the category of the product"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the product is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identification for the order")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identification of the customer"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of the order"),
                    RenewalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenewedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the shipment"),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the arrival"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true, comment: "Identification of the address"),
                    SubscriptionBoxId = table.Column<int>(type: "int", nullable: true, comment: "Identification of the subscriptionBox"),
                    IsSubscriptionCanceled = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the subscription is canceled")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_SubscriptionBoxes_SubscriptionBoxId",
                        column: x => x.SubscriptionBoxId,
                        principalTable: "SubscriptionBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSubscriptionBoxes",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionBoxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubscriptionBoxes", x => new { x.ProductId, x.SubscriptionBoxId });
                    table.ForeignKey(
                        name: "FK_ProductSubscriptionBoxes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSubscriptionBoxes_SubscriptionBoxes_SubscriptionBoxId",
                        column: x => x.SubscriptionBoxId,
                        principalTable: "SubscriptionBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsOrders",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the product"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Identification of the order"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of the product")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsOrders", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "388872d3-f844-4e1c-8495-9be0fb610b1b", "d7a8fca8-2bb2-4a3c-bbe9-96b71f7c633b", "IdentityRole", "User", "USER" },
                    { "78b01d24-5600-4697-b852-aa2a3faf3c2f", "aba1f775-6184-4c82-996d-d66a49508ec8", "IdentityRole", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b", 0, "cd3c1ede-9fbe-4c46-81c6-c754507a4a0b", "admin@gmail.com", true, false, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEKuiZa26qJQnnFaY2nTw8E6zEz6JyxXK7mBpsbxhrMBpH23kqkniOqV8IU8MbQ9ypQ==", null, false, "ADMIN@GMAIL.COM", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wet Food" },
                    { 2, "Dry Food" },
                    { 3, "Supplement" },
                    { 4, "Toy" },
                    { 5, "Accessory" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Not finalized" },
                    { 2, "In Progress" },
                    { 3, "Shipped" },
                    { 4, "In Delivery Center" },
                    { 5, "Picked up" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionBoxes",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Promote strong bones and teeth with this subscription box.", "https://i.ibb.co/yXPZG4G/1.png", "Bone Health", 15.49m },
                    { 2, "Enhance your cat's coat health for a shiny and lustrous fur.", "https://i.ibb.co/0QMLRbh/2.png", "Silky Fur", 17.49m },
                    { 3, "Support healthy digestion and bowel movements with this subscription box.", "https://i.ibb.co/GQQBknL/3.png", "Bowl Movement", 15.99m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsDeleted", "IsSubscription", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 3, "Essential cat calcium supplement for strong bones and teeth.", "https://i.ibb.co/frhS8TM/Catio-com-2.png", false, true, "Calcium", 8.99m },
                    { 2, 3, "Boost your cat's coat and skin health with our Omega-3 supplement. Promotes a shiny coat and supports overall well-being.", "https://i.ibb.co/DzdTWbZ/6.png", false, true, "Omega-3", 9.99m },
                    { 3, 3, "Maintain healthy digestion for your cat with our fiber supplement. Supports bowel regularity and digestive balance.", "https://i.ibb.co/wLCVbkz/Catio-com-3.png", false, true, "Fiber", 7.99m },
                    { 4, 1, "Delicious wet cat food with real chicken for a savory dining experience.", "https://i.ibb.co/6PNYp8Q/Wet-Chicken.png", false, true, "Wet Chicken", 2.99m },
                    { 5, 2, "Nutritious dry cat food with chicken as the main ingredient. Supports overall health.", "https://i.ibb.co/vjHdjs2/Dry-Chicken.png", false, true, "Dry Chicken", 2.50m },
                    { 6, 1, "Tasty wet cat food featuring real fish to satisfy your cat's seafood cravings.", "https://i.ibb.co/vVXjkg7/Wet-Fish.png", false, true, "Wet Fish", 3.99m },
                    { 7, 2, "High-quality dry cat food with fish for a protein-rich and flavorful meal.", "https://i.ibb.co/JshpRy2/Dry-Fish.png", false, true, "Dry Fish", 3.50m },
                    { 8, 1, "Irresistible wet cat food with real beef, providing a source of premium protein.", "https://i.ibb.co/thzxbh1/Wet-Beef.png", false, true, "Wet Beef", 3.99m },
                    { 9, 2, "Wholesome dry cat food featuring beef for a balanced and tasty diet.", "https://i.ibb.co/0FSfBhf/Dry-Beef.png", false, true, "Dry Beef", 3.50m },
                    { 10, 2, "Perfectly balanced dry cat food with a blend of chicken and turkey for optimal nutrition.", "https://i.ibb.co/XL7NV1D/Dry-Chicken-And-Turkey.png", false, true, "Dry Chicken & Turkey", 4.99m },
                    { 11, 1, "Indulge your cat with wet food featuring a delightful combination of salmon and chicken.", "https://i.ibb.co/WK3QYZ5/Wet-Chicken-And-Salmon.png", false, true, "Wet Salmon & Chicken", 3.99m },
                    { 12, 4, "A multi-level cat tree designed to keep your feline friend entertained and comfortable.", "https://i.ibb.co/Jy1fQrV/Cat-Tree.png", false, false, "Cat Tree", 25.99m },
                    { 13, 5, "A stylish food bowl perfect for serving your cat's favorite wet and dry food.", "https://i.ibb.co/yy4md2H/Food-Bowl.png", false, false, "Food Bowl", 5.45m },
                    { 14, 4, "An interactive fishing rod toy that will engage your cat in playful antics for hours.", "https://i.ibb.co/tm04yZf/Fishing-Rod-Toy.png", false, false, "Fishing Rod Toy", 2.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductSubscriptionBoxes",
                columns: new[] { "ProductId", "SubscriptionBoxId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 1 },
                    { 7, 2 },
                    { 8, 2 },
                    { 10, 3 },
                    { 11, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SubscriptionBoxId",
                table: "Orders",
                column: "SubscriptionBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsOrders_ProductId",
                table: "ProductsOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubscriptionBoxes_SubscriptionBoxId",
                table: "ProductSubscriptionBoxes",
                column: "SubscriptionBoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ProductsOrders");

            migrationBuilder.DropTable(
                name: "ProductSubscriptionBoxes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "SubscriptionBoxes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
