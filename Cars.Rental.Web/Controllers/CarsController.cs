using System;
using System.Net;
using System.Threading.Tasks;
using Cars.Rental.Models;
using Cars.Rental.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Rental.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CarsController : ControllerBase
	{
		private readonly ICarsRentalPriceService _carsService;

		public CarsController(ICarsRentalPriceService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(CarRentalPriceModel), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get([FromQuery]int carId, [FromQuery]DateTime start, [FromQuery]DateTime end)
		{
			var carRentalPrice = await _carsService.GetCarPriceBreakdown(carId, start, end);

			return Ok(carRentalPrice);
		}
	}
}
