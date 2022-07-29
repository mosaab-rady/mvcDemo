using mvcApp.Models;

namespace mvcApp.Services;

public interface IProductsService
{
	public Task<IEnumerable<Product>> GetAllProductsAsync();
	public Task<Product?> GetProductByIdAsync(Guid Id);
	public Task CreateProductAsync(Product product);
	public Task UpdateProductByIdAsync(Guid Id, Product product);
	public Task DeleteProductByIdAsync(Guid Id);
}