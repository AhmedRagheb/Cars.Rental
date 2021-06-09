using Cars.Rental.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cars.Rental.Web.Settings
{
	internal static class DatabaseSettings
	{
		public static IServiceCollection AddDatabase(this IServiceCollection services)
		{
			services.AddDbContext<CarsDbContext>(options =>
			{
				options.UseInMemoryDatabase("cars-db");
			});

			return services;
		}

		public static IApplicationBuilder UseDatabase(this IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
			var context = serviceScope.ServiceProvider.GetRequiredService<CarsDbContext>();
			
			context.Database.EnsureCreated();

			return app;
		}
	}
}
