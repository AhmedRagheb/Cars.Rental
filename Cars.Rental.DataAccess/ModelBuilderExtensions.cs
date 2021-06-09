using Cars.Rental.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cars.Rental.DataAccess
{
	internal static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			var cars = new List<Car>
				{
					new Car
					{
						Id = 1,
						IsAvailable = true,
						RentalPerDay = 15.0
					},
					new Car
					{
						Id = 2,
						IsAvailable = true,
						RentalPerDay = 16.5
					},
					new Car
					{
						Id = 3,
						IsAvailable = true,
						RentalPerDay = 19.0
					},
					new Car
					{
						Id = 4,
						IsAvailable = false,
						RentalPerDay = 25.5
					},
					new Car
					{
						Id = 5,
						IsAvailable = true,
						RentalPerDay = 22.5
					}
				};

			modelBuilder.Entity<Car>().HasData(cars);
		}
	}
}
