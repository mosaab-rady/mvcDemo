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

	public DbSet<Fabric> Fabrics { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Fabric>(entity =>
		{
			entity.HasKey(e => e.Name);
			entity.HasIndex(e => e.Name).IsUnique(true);

			entity.Property(e => e.AntiBilling).HasDefaultValue(false);
			entity.Property(e => e.ButterySoft).HasDefaultValue(false);
			entity.Property(e => e.Stretch).HasDefaultValue(false);

		});
	}

}

