using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// This method registers all services with their interfaces and implementations of given assembly.
        /// The assembly is taken from the type of random service implementation provider.
        /// </summary>
        /// <param name = "serviceType">Type of random service implementation.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided");
            }

            Type[] implementationTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (var implementationType in implementationTypes)
            {
                Type? interfaceType = implementationType
                    .GetInterface($"I{implementationType.Name}");

                if (interfaceType == null)
                {
                    throw new InvalidOperationException("No interface provided");
                }

                services.AddScoped(interfaceType, implementationType);
            }
        }

        /// <summary>
        /// Adds the CatFoodSubscriptionDbContext with the specified connection string.
        /// </summary>
        public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CatFoodSubscriptionDbContext>(options => options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Adds the default Identity for the Customer type with specified options to the service collection.
        /// </summary>
        public static void AddApplicationIdentity(this IServiceCollection services)
        {
            services
                .AddDefaultIdentity<Customer>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                })
                .AddRoles<IdentityRole<string>>()
                .AddEntityFrameworkStores<CatFoodSubscriptionDbContext>();
        }
    }
}
