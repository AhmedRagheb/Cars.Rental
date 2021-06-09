using Cars.Rental.Services.Abstractions;

namespace Cars.Rental.Services
{
	public class RentalDiscountService : IRentalDiscountService
	{
		/// <summary>
		/// if a rental takes longer than 3 days, the renter gets a discount of 15% over the total price
		/// </summary>
		/// <param name="numberOfDays">number of rent days</param>
		/// <param name="price">total rent price</param>
		/// <returns></returns>
		public double CalculateDiscountForLongPeriodRent(double numberOfDays, double price)
		{
			if (numberOfDays > 3)
			{
				var discountPrice = price * .15;
				price -= discountPrice;
			}

			return price;
		}
	}
}
