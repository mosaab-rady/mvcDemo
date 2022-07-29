using System.ComponentModel.DataAnnotations;

namespace mvcApp.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Type { get; set; } = null!;
		public string? Summary { get; set; }
	}
}



public record CreateProduct(
										[Required] string Name,
										[Range(1, 1000)] decimal Price,
										[Required] string Type,
										string? Summary
										);