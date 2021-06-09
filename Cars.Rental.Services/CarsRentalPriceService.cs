using Cars.Rental.DataAccess;
using Cars.Rental.Models;
using Cars.Rental.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Rental.Services
{
	public class CarsRentalPriceService : ICarsRentalPriceService
	{
		private readonly CarsDbContext _carsDbContext;
		private readonly IRentalDiscountService _rentalDiscountService;
		private readonly IRentalAdditionalCostsService _rentalAdditionalCostsService;

		public CarsRentalPriceService(
			CarsDbContext carsDbContext,
			IRentalDiscountService rentalDiscountService,
			IRentalAdditionalCostsService rentalAdditionalCostsService)
		{
			_carsDbContext = carsDbContext;
			_rentalDiscountService = rentalDiscountService;
			_rentalAdditionalCostsService = rentalAdditionalCostsService;
		}

		public async Task<CarRentalPriceModel> GetCarPriceBreakdown(int carId, DateTime start, DateTime end)
		{
			ValidateInputDates(start, end);

			var baseCarPricePerDay = await GetCarBasePrice(carId);

			var numberOfDays = (end - start).TotalDays;

			var additionalCosts = _rentalAdditionalCostsService.CalculateAdditionalCosts(start, end, baseCarPricePerDay);

			var totalRentalPrice = (baseCarPricePerDay * numberOfDays) + additionalCosts;

			var discountedPrice = _rentalDiscountService.CalculateDiscountForLongPeriodRent(numberOfDays, totalRentalPrice);

			return Map(discountedPrice);
		}

		private async Task<double> GetCarBasePrice(int carId)
		{
			var car = await _carsDbContext.Cars.Where(c => c.IsAvailable).SingleOrDefaultAsync(c => c.Id == carId);

			if (car == null)
			{
				throw new Exception("car not available or not exist");
			}

			var baseCarPricePerDay = car.RentalPerDay;

			return baseCarPricePerDay;
		}

		private CarRentalPriceModel Map(double price)
		{
			return new CarRentalPriceModel
			{
				TotalRentPrice = Utilities.RoundPrice(price)
			};
		}

		private static void ValidateInputDates(DateTime start, DateTime end)
		{
			if (start > end)
			{
				throw new Exception("start date bigger than end date");
			}
		}
	}
}
