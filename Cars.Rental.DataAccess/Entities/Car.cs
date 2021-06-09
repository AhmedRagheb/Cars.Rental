namespace Cars.Rental.DataAccess.Entities
{
	public class Car
	{
		public int Id { get; set; }
		public bool IsAvailable { get; set; }
		public double RentalPerDay { get; set; }
	}
}
