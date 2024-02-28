namespace CatFoodSubscription.Common
{
    public static class ValidationConstants
    {
        private const string projectStart = "01/22/2024";

        public class CustomerConstants
        {
            //FirstName
            public const int CustomerFirstNameMinLength = 3;
            public const int CustomerFirstNameMaxLength = 50;

            //LastName
            public const int CustomerLastNameMinLength = 3;
            public const int CustomerLastNameMaxLength = 50;
        }

        public class AddressConstants
        {
            //Country
            public const int AddressCountryMinLength = 4;
            public const int AddressCountryMaxLength = 56;

            //City
            public const int AddressCityMinLength = 3;
            public const int AddressCityMaxLength = 58;

            //Street
            public const int AddressStreetMinLength = 3;
            public const int AddressStreetMaxLength = 250;
        }

        public class ProductConstants
        {
            //Name
            public const int ProductNameMinLength = 3;
            public const int ProductNameMaxLength = 50;

            //Description
            public const int ProductDescriptionMinLength = 3;
            public const int ProductDescriptionMaxLength = 250;
        }

        public class CategoryConstants
        {
            //Name
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 25;
        }

        public class SubscriptionBoxConstants
        {
            //Name           
            public const int SubscriptionBoxNameMinLength = 3;
            public const int SubscriptionBoxNameMaxLength = 50;

            //Description
            public const int SubscriptionBoxDescriptionMinLength = 3;
            public const int SubscriptionBoxDescriptionMaxLength = 250;
        }

        public class StatusConstants
        {
            //Name
            public const int StatusNameMinLength = 3;
            public const int StatusNameMaxLength = 50;
        }
    }
}
