using System;
using Cars.Rental.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Cars.Rental.Services.Tests
{
	public abstract class InMemoryShowsDbContext
	{
		protected CarsDbContext DbContext { get; set; }

		protected InMemoryShowsDbContext()
		{
			var options = new DbContextOptionsBuilder<CarsDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.EnableSensitiveDataLogging()
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.Options;

			DbContext = new CarsDbContext(options);
		}
	}
}
