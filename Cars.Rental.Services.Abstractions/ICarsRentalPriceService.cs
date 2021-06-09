using Cars.Rental.Models;
using System;
using System.Threading.Tasks;

namespace Cars.Rental.Services.Abstractions
{
	public interface ICarsRentalPriceService
	{
		Task<CarRentalPriceModel> GetCarPriceBreakdown(int carId, DateTime start, DateTime end);
	}
}
