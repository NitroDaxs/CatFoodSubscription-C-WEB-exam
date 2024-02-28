using CatFoodSubscription.Data.Models;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class SeedData
    {
        public ICollection<Category> GetCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Wet Food"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Dry Food"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Supplement"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Toy"
            };
            categories.Add(category);

            return categories;
        }

        public ICollection<Status> GetStatuses()
        {
            ICollection<Status> statuses = new HashSet<Status>();

            Status status;
            status = new Status()
            {
                Id = 1,
                Name = "In Progress"
            };
            statuses.Add(status);

            status = new Status()
            {
                Id = 2,
                Name = "Shipped"
            };
            statuses.Add(status);

            status = new Status()
            {
                Id = 3,
                Name = "In Delivery Center"
            };
            statuses.Add(status);

            status = new Status()
            {
                Id = 4,
                Name = "Picked up"
            };
            statuses.Add(status);

            return statuses;
        }

        public ICollection<Product> GetProducts()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product()
            {
                Id = 1,
                Name = "Calcium",
                ImageUrl = "https://i.ibb.co/frhS8TM/Catio-com-2.png",
                Description = "Essential cat calcium supplement for strong bones and teeth.",
                Price = 8.99m,
                CategoryId = 3
            };
            products.Add(product);

            product = new Product()
            {
                Id = 2,
                Name = "Omega-3",
                ImageUrl = "https://i.ibb.co/DzdTWbZ/6.png",
                Description = "Boost your cat's coat and skin health with our Omega-3 supplement. Promotes a shiny coat and supports overall well-being.",
                Price = 9.99m,
                CategoryId = 3
            };
            products.Add(product);

            product = new Product()
            {
                Id = 3,
                Name = "Fiber",
                ImageUrl = "https://i.ibb.co/wLCVbkz/Catio-com-3.png",
                Description = "Maintain healthy digestion for your cat with our fiber supplement. Supports bowel regularity and digestive balance.",
                Price = 7.99m,
                CategoryId = 3
            };
            products.Add(product);

            product = new Product()
            {
                Id = 4,
                Name = "Wet Chicken",
                ImageUrl = "https://i.ibb.co/6PNYp8Q/Wet-Chicken.png",
                Description = "Delicious wet cat food with real chicken for a savory dining experience.",
                Price = 2.99m,
                CategoryId = 1
            };
            products.Add(product);

            product = new Product()
            {
                Id = 5,
                Name = "Dry Chicken",
                ImageUrl = "https://i.ibb.co/vjHdjs2/Dry-Chicken.png",
                Description = "Nutritious dry cat food with chicken as the main ingredient. Supports overall health.",
                Price = 2.50m,
                CategoryId = 2
            };
            products.Add(product);

            product = new Product()
            {
                Id = 6,
                Name = "Wet Fish",
                ImageUrl = "https://i.ibb.co/vVXjkg7/Wet-Fish.png",
                Description = "Tasty wet cat food featuring real fish to satisfy your cat's seafood cravings.",
                Price = 3.99m,
                CategoryId = 1
            };
            products.Add(product);

            product = new Product()
            {
                Id = 7,
                Name = "Dry Fish",
                ImageUrl = "https://i.ibb.co/JshpRy2/Dry-Fish.png",
                Description = "High-quality dry cat food with fish for a protein-rich and flavorful meal.",
                Price = 3.50m,
                CategoryId = 2
            };
            products.Add(product);

            product = new Product()
            {
                Id = 8,
                Name = "Wet Beef",
                ImageUrl = "https://i.ibb.co/thzxbh1/Wet-Beef.png",
                Description = "Irresistible wet cat food with real beef, providing a source of premium protein.",
                Price = 3.99m,
                CategoryId = 1
            };
            products.Add(product);

            product = new Product()
            {
                Id = 9,
                Name = "Dry Beef",
                ImageUrl = "https://i.ibb.co/0FSfBhf/Dry-Beef.png",
                Description = "Wholesome dry cat food featuring beef for a balanced and tasty diet.",
                Price = 3.50m,
                CategoryId = 2
            };
            products.Add(product);

            product = new Product()
            {
                Id = 10,
                Name = "Dry Chicken & Turkey",
                ImageUrl = "https://i.ibb.co/XL7NV1D/Dry-Chicken-And-Turkey.png",
                Description = "Perfectly balanced dry cat food with a blend of chicken and turkey for optimal nutrition.",
                Price = 4.99m,
                CategoryId = 2
            };
            products.Add(product);

            product = new Product()
            {
                Id = 11,
                Name = "Wet Salmon & Chicken",
                ImageUrl = "https://i.ibb.co/WK3QYZ5/Wet-Chicken-And-Salmon.png",
                Description = "Indulge your cat with wet food featuring a delightful combination of salmon and chicken.",
                Price = 3.99m,
                CategoryId = 1
            };
            products.Add(product);

            return products;
        }

        public ICollection<SubscriptionBox> GetSubscriptionBoxes()
        {
            ICollection<SubscriptionBox> subscriptionBoxes = new HashSet<SubscriptionBox>();

            SubscriptionBox subscriptionBox;

            subscriptionBox = new SubscriptionBox()
            {
                Id = 1,
                Name = "Bone Health",
                ImageUrl = "https://i.ibb.co/yXPZG4G/1.png",
                Description = "Promote strong bones and teeth with this subscription box.",
                Price = 15.49m,
                ProductSubscriptionBoxes = new List<ProductSubscriptionBox>()
            };
            subscriptionBoxes.Add(subscriptionBox);

            subscriptionBox = new SubscriptionBox()
            {
                Id = 2,
                Name = "Silky Fur",
                ImageUrl = "https://i.ibb.co/0QMLRbh/2.png",
                Description = "Enhance your cat's coat health for a shiny and lustrous fur.",
                Price = 17.49m,
                ProductSubscriptionBoxes = new List<ProductSubscriptionBox>()
            };
            subscriptionBoxes.Add(subscriptionBox);

            subscriptionBox = new SubscriptionBox()
            {
                Id = 3,
                Name = "Bowl Movement",
                ImageUrl = "https://i.ibb.co/GQQBknL/3.png",
                Description = "Support healthy digestion and bowel movements with this subscription box.",
                Price = 15.99m,
                ProductSubscriptionBoxes = new List<ProductSubscriptionBox>()
            };
            subscriptionBoxes.Add(subscriptionBox);

            return subscriptionBoxes;
        }

        public ICollection<ProductSubscriptionBox> GetProductSubscriptionBoxes()
        {
            ICollection<ProductSubscriptionBox> productSubscriptionBoxes = new HashSet<ProductSubscriptionBox>();

            ProductSubscriptionBox productSubscriptionBox;

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 1,
                SubscriptionBoxId = 1
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 5,
                SubscriptionBoxId = 1
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 11,
                SubscriptionBoxId = 1
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 2,
                SubscriptionBoxId = 2
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 7,
                SubscriptionBoxId = 2
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 8,
                SubscriptionBoxId = 2
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 3,
                SubscriptionBoxId = 3
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 4,
                SubscriptionBoxId = 3
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            productSubscriptionBox = new ProductSubscriptionBox
            {
                ProductId = 10,
                SubscriptionBoxId = 3
            };
            productSubscriptionBoxes.Add(productSubscriptionBox);

            return productSubscriptionBoxes;
        }
    }
}
