using System;

namespace Cars.Rental.Services
{
	public static class Utilities
	{
		public static double RoundPrice(double price)
		{
			return Math.Round(price, 3);
		}
	}
}
