using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
		}

		public DbSet<Company> Companies { get; set; }
	}
}
