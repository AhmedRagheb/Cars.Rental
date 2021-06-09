namespace Cars.Rental.Services.Abstractions
{
	public interface IRentalDiscountService
	{
		double CalculateDiscountForLongPeriodRent(double numberOfDays, double price);
	}
}
