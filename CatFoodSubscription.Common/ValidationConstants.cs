namespace CatFoodSubscription.Common
{
    public static class ValidationConstants
    {
        private const string projectStart = "01/22/2024";
        public const string errorLengthMessage = "{0} must be between {2} and {1} characters!";
        public const string errorRequiredMessage = "The field {0} is requiered!";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
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

            //Street
            public const int AddressPostalCodeMinLength = 3;
            public const int AddressPostalCodeMaxLength = 10;

            //PhoneNumber
            public const string PhoneNumberRegEx = @"^(?:\+359|0)\s?[87-9][0-9]\s?\d{3}\s?\d{4}$";
            public const string PhoneNumberErrorMessage = "The phone number format is incorrect. Example: +359 87 999 9999 or 087 999 9999";

            //FirstName
            public const int CustomerFirstNameMinLength = 3;
            public const int CustomerFirstNameMaxLength = 50;

            //LastName
            public const int CustomerLastNameMinLength = 3;
            public const int CustomerLastNameMaxLength = 50;

            //Email
            public const string EmailRegEx = @"^[\w\.]+@([\w-]+\.)+[\w-]{2,4}$";
            public const string EmailErrorMessage = "The email format is incorrect. Example: email@example.com";
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

        public class Roles
        {
            public const string UserRoleName = "User";
            public const string AdminRoleName = "Admin";
        }
    }
}
