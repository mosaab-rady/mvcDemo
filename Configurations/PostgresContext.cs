using Microsoft.EntityFrameworkCore;
using mvcApp.Models;

namespace mvcApp.Configurations;

public partial class PostgresContext : DbContext
{
	public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
	{
	}

	public virtual DbSet<Product> Products { get; set; } = null!;




	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("uuid-ossp");

		modelBuilder.Entity<Product>(entity =>
		{
			entity.ToTable("products");

			entity.HasIndex(e => e.Name, "ix_products_name")
								.IsUnique();

			entity.Property(e => e.Id)
								.HasColumnName("id")
								.HasDefaultValueSql("uuid_generate_v4()");

			entity.Property(e => e.Name).HasColumnName("name");

			entity.Property(e => e.Price).HasColumnName("price");

			entity.Property(e => e.Summary).HasColumnName("summary");

			entity.Property(e => e.Type).HasColumnName("type");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

