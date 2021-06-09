using Cars.Rental.Services.Abstractions;
using System;

namespace Cars.Rental.Services
{
	public class RentalAdditionalCostsService : IRentalAdditionalCostsService
	{
		/// <summary>
		/// Insurance adds 10% per day on top of the car price.
		/// Adds 10% per day on top of the car price.
		/// On Saturday and Sunday the base price of the car goes up with 5%.
		/// </summary>
		/// <param name="baseCarPricePerDay">base car price per day</param>
		/// <param name="start">start date of rental</param>
		/// <param name="end">end date of rental</param>
		/// <returns>price with addition costs</returns>
		public double CalculateAdditionalCosts(DateTime start, DateTime end, double baseCarPricePerDay)
		{
			var totalAdditionalCosts = 0.0;
			for (var date = start; date < end; date = date.AddDays(1))
			{
				var weekendExtra = 0.0;
				if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
				{
					weekendExtra = (baseCarPricePerDay * .05);
				}

				var insurance = baseCarPricePerDay * .1;
				var additionPerDay = baseCarPricePerDay * .1;

				totalAdditionalCosts += insurance + additionPerDay + weekendExtra;
			}

			return totalAdditionalCosts;
		}
	}
}
