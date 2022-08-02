using mvcApp.Models;

namespace mvcApp.Services;
public interface IFabricService
{
	Task<IEnumerable<Fabric>> GetFabricsAsync();
	Task<Fabric> GetFabricByNameAsync(string name);
	Task<Fabric> CreateFabricAsync(Fabric fabric);
}