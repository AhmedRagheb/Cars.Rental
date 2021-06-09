using FluentAssertions;
using System;
using Xunit;

namespace Cars.Rental.Services.Tests
{
	public class RentalAdditionalCostsServiceTests
	{
		private readonly RentalAdditionalCostsService _rentalAdditionalCostsService;

		public RentalAdditionalCostsServiceTests()
		{
			_rentalAdditionalCostsService = new RentalAdditionalCostsService();
		}

		[Fact]
		public void CalculateAdditionalCosts_IncludeWeekend_Should_Return_Price()
		{
			// act
			var actual = _rentalAdditionalCostsService.CalculateAdditionalCosts(new DateTime(2021, 02, 15), new DateTime(2021, 02, 19), 15);

			// assert
			actual.Should().Be(12.0);
		}

		[Fact]
		public void CalculateAdditionalCosts_Should_Return_Price()
		{
			// act
			var actual = _rentalAdditionalCostsService.CalculateAdditionalCosts(new DateTime(2021, 02, 15), new DateTime(2021, 02, 17), 7.5);

			// assert
			actual.Should().Be(3.0);
		}
	}
}
