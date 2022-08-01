using Microsoft.EntityFrameworkCore;
using mvcApp.Configurations;
using mvcApp.middleware;
using mvcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IsLoggedInMiddleware>();

// connect to database
builder.Services.AddDbContext<PostgresContext>(options =>
	options
					.UseNpgsql(
						builder.Configuration.GetSection("PostgresDB").Get<PostgresDbSettings>().ConnectionString
						)
					.UseSnakeCaseNamingConvention()
					.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
					.EnableSensitiveDataLogging()
);

builder.Services.AddScoped<IProductsService, PostgresProductsService>();
builder.Services.AddScoped<IUsersService, PostgresUsersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseExceptionHandler("/Home/Error");

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<IsLoggedInMiddleware>();

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
