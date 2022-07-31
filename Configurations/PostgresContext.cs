using Microsoft.EntityFrameworkCore;
using mvcApp.Models;

namespace mvcApp.Configurations;

public partial class PostgresContext : DbContext
{
	public PostgresContext()
	{

	}
	public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
	{
	}

	public virtual DbSet<Product> Products { get; set; } = null!;

	public virtual DbSet<User> Users { get; set; } = null!;

}

