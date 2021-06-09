using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Cars.Rental.Web.Settings
{
	internal static class SwaggerSettings
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cars API", Version = "v1" });
			});

			services.ConfigureSwaggerGen(options =>
			{
				options.OrderActionsBy((apiDesc) => $"{apiDesc.RelativePath}");
			});

			return services;
		}

		internal static IApplicationBuilder UseSwaggerSettings(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(
				options =>
				{
					options.EnableDeepLinking();
					options.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
				});

			return app;
		}
	}
}
