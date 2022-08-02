using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcApp.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Type { get; set; }
		public string Summary { get; set; }
		public string FabricName { get; set; }
		[ForeignKey("FabricName")]
		public Fabric Fabric { get; set; }
	}
}



public record CreateProduct(
										[Required] string Name,
										[Range(1, 1000)] decimal Price,
										[Required] string Type,
										string Summary,
										string FabricName
										);