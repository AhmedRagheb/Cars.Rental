using FluentAssertions;
using Xunit;

namespace Cars.Rental.Services.Tests
{
	public class RentalDiscountServiceTests
	{
		private readonly RentalDiscountService _rentalDiscountService;

		public RentalDiscountServiceTests()
		{
			_rentalDiscountService = new RentalDiscountService();
		}

		[Fact]
		public void CalculateDiscountForThreeDays_Should_Return_PriceWithDicount()
		{
			// act
			var actual = _rentalDiscountService.CalculateDiscountForLongPeriodRent(4, 170);

			// assert
			actual.Should().Be(144.5);
		}

		[Fact]
		public void CalculateDiscountForLessThanThreeDays_Should_Return_PriceWithoutDicount()
		{
			// act
			var actual = _rentalDiscountService.CalculateDiscountForLongPeriodRent(2, 130.5);

			// assert
			actual.Should().Be(130.5);
		}
	}
}
