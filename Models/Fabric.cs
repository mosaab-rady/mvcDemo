using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvcApp.Models;

public class Fabric
{
	public string Name { get; set; }
	public List<string> MaterialAndCare { get; set; }
	[Required]
	[DisplayName("Why we made this")]
	public string WhyWeMadeThis { get; set; }
	public Boolean Stretch { get; set; }
	public Boolean AntiBilling { get; set; }
	public Boolean ButterySoft { get; set; }
}