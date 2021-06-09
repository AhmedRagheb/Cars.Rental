using System;

namespace Cars.Rental.Services.Abstractions
{
	public interface IRentalAdditionalCostsService
	{
		double CalculateAdditionalCosts(DateTime start, DateTime end, double baseCarPricePerDay);
	}
}
