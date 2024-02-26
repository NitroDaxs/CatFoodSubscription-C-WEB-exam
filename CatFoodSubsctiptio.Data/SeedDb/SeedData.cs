using CatFoodSubscription.Data.Models;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class SeedData
    {
        public Category FoodCategory { get; set; }
        public Category SupplementCategory { get; set; }
        public Category ToyCategory { get; set; }


        public SeedData()
        {

        }

        private void SeedCategories()
        {
            FoodCategory = new Category()
            {
                Id = 1,
                Name = "Food"
            };

            SupplementCategory = new Category()
            {
                Id = 2,
                Name = "Supplement"
            };

            ToyCategory = new Category()
            {
                Id = 3,
                Name = "Toy"
            };
        }
    }
}
