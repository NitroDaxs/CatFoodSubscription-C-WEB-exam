using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddApplicationDbContext(connectionString);
builder.Services.AddApplicationIdentity();

builder.Services.AddApplicationServices(typeof(IOrderService));

builder.Services
    .AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.AsignAdmin();

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
