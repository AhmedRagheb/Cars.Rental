using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Cars.Rental.DataAccess.Entities;

namespace Cars.Rental.DataAccess
{
	public class CarsDbContext : DbContext
	{
		public virtual DbSet<Car> Cars { get; set; }

		public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
		{
		}

		public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await Database.BeginTransactionAsync();
		}

		public virtual async Task<int> SaveChangesAsync()
		{
			try
			{
				var result = await base.SaveChangesAsync();

				return result;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Seed();
		}
	}
}
