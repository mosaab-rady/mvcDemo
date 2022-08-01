using mvcApp.Models;

namespace mvcApp.Services;

public interface IProductsService
{
	Task<IEnumerable<Product>> GetAllProductsAsync();
	Task<Product> GetProductByIdAsync(Guid Id);
	Task CreateProductAsync(Product product);
	Task UpdateProductByIdAsync(Guid Id, Product product);
	Task DeleteProductByIdAsync(Guid Id);
}