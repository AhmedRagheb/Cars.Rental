using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Cars.Rental.DataAccess.Entities;

namespace Cars.Rental.Services.Tests
{
	public class CarsRentalPriceServiceTests : InMemoryShowsDbContext
	{
		private readonly CarsRentalPriceService _carsService;

		public CarsRentalPriceServiceTests()
		{
			var rentalDiscountService = new RentalDiscountService();
			var rentalAdditionalCostsService = new RentalAdditionalCostsService();

			_carsService = new CarsRentalPriceService(DbContext, rentalDiscountService, rentalAdditionalCostsService);
		}

		[Fact]
		public async Task GetCarPriceBreakdown_IncludeWeekend_Should_Return_Price()
		{
			// prepare
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
				}
			};

			await DbContext.Cars.AddRangeAsync(cars);
			await DbContext.SaveChangesAsync();

			// act
			var actual = await _carsService.GetCarPriceBreakdown(1, new DateTime(2021, 02, 10), new DateTime(2021, 02, 15));

			// assert
			actual.TotalRentPrice.Should().Be(77.775);
		}

		[Fact]
		public async Task GetCarPriceBreakdown_LongRent_Should_Return_Price()
		{
			// prepare
			var cars = new List<Car>
			{
				new Car
				{
					Id = 1,
					IsAvailable = true,
					RentalPerDay = 17.0
				}
			};

			await DbContext.Cars.AddRangeAsync(cars);
			await DbContext.SaveChangesAsync();

			// act
			var actual = await _carsService.GetCarPriceBreakdown(1, new DateTime(2021, 02, 15), new DateTime(2021, 02, 19));

			// assert
			actual.TotalRentPrice.Should().Be(69.36);
		}

		[Fact]
		public async Task GetCarPriceBreakdown_ShortRent_Should_Return_Price()
		{
			// prepare
			var cars = new List<Car>
			{
				new Car
				{
					Id = 1,
					IsAvailable = true,
					RentalPerDay = 9.3
				}
			};

			await DbContext.Cars.AddRangeAsync(cars);
			await DbContext.SaveChangesAsync();

			// act
			var actual = await _carsService.GetCarPriceBreakdown(1, new DateTime(2021, 02, 15), new DateTime(2021, 02, 18));

			// assert
			actual.TotalRentPrice.Should().Be(33.48);
		}

		[Fact]
		public async Task GetCarPriceBreakdown_ShortRentDuringWeekEnd_Should_Return_Price()
		{
			// prepare
			var cars = new List<Car>
			{
				new Car
				{
					Id = 1,
					IsAvailable = true,
					RentalPerDay = 7.3
				},
				new Car
				{
					Id = 3,
					IsAvailable = true,
					RentalPerDay = 11.5
				}
			};

			await DbContext.Cars.AddRangeAsync(cars);
			await DbContext.SaveChangesAsync();

			// act
			var actual = await _carsService.GetCarPriceBreakdown(3, new DateTime(2021, 02, 12), new DateTime(2021, 02, 14));

			// assert
			actual.TotalRentPrice.Should().Be(28.175);
		}

		[Fact]
		public async Task GetCarPriceBreakdown_WithStartDateBiggerThanEndDate_Should_ThrowException()
		{
			// act
			var actualException = await Assert.ThrowsAsync<Exception>(() => _carsService.GetCarPriceBreakdown(1, new DateTime(2021, 02, 15), new DateTime(2021, 02, 10)));

			// assert
			actualException.Message.Should().Be("start date bigger than end date");
		}

		[Fact]
		public async Task GetCarPriceBreakdown_ForNotAvailable_Should_ThrowException()
		{
			// act
			var actualException = await Assert.ThrowsAsync<Exception>(() => _carsService.GetCarPriceBreakdown(1, new DateTime(2021, 02, 01), new DateTime(2021, 02, 02)));

			// assert
			actualException.Message.Should().Be("car not available or not exist");
		}
	}
}
