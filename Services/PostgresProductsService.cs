using Microsoft.EntityFrameworkCore;
using mvcApp.Configurations;
using mvcApp.Models;

namespace mvcApp.Services;

public class PostgresProductsService : IProductsService
{
	private readonly PostgresContext _context;

	public PostgresProductsService(PostgresContext context)
	{
		_context = context;
	}




	public async Task<IEnumerable<Product>> GetAllProductsAsync()
	{
		var products = await _context.Products.ToListAsync();
		return products;
	}
	public async Task<Product> GetProductByIdAsync(Guid Id)
	{
		var product = await _context.Products.Include(p => p.Fabric).SingleOrDefaultAsync(p => p.Id == Id);
		return product;
	}

	public async Task CreateProductAsync(Product product)
	{
		await _context.Products.AddAsync(product);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteProductByIdAsync(Guid Id)
	{
		Product product = await _context.Products.FindAsync(Id);
		if (product is not null)
			_context.Products.Remove(product);
		await _context.SaveChangesAsync();

	}

	public async Task UpdateProductByIdAsync(Guid Id, Product product)
	{
		var existingProduct = await _context.Products.FindAsync(Id);
		existingProduct = product;
		await _context.SaveChangesAsync();
	}
}
