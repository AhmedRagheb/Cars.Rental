using Microsoft.Extensions.DependencyInjection;
using Cars.Rental.Services;
using Cars.Rental.Services.Abstractions;

namespace Cars.Rental.Web.Settings
{
	internal static class DependencyInjectionSettings
	{
		public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
		{
			services.AddTransient<ICarsRentalPriceService, CarsRentalPriceService>();
			services.AddSingleton<IRentalAdditionalCostsService, RentalAdditionalCostsService>();
			services.AddSingleton<IRentalDiscountService, RentalDiscountService>();

			return services;
		}
	}
}
