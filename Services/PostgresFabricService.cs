using Microsoft.EntityFrameworkCore;
using mvcApp.Configurations;
using mvcApp.Models;

namespace mvcApp.Services;
public class PostgresFabricService : IFabricService
{
	private readonly PostgresContext _context;

	public PostgresFabricService(PostgresContext context)
	{
		_context = context;
	}

	public async Task<Fabric> CreateFabricAsync(Fabric fabric)
	{
		await _context.AddAsync(fabric);
		await _context.SaveChangesAsync();
		return fabric;
	}

	public async Task<Fabric> GetFabricByNameAsync(string name)
	{
		var fabric = await _context.Fabrics.SingleOrDefaultAsync(f => f.Name == name);
		return fabric;
	}

	public async Task<IEnumerable<Fabric>> GetFabricsAsync()
	{
		var Fabrics = await _context.Fabrics.ToListAsync();
		return Fabrics;
	}
}
