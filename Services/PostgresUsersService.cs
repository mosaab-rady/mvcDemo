using Microsoft.EntityFrameworkCore;
using mvcApp.Configurations;
using mvcApp.Models;

namespace mvcApp.Services;

public class PostgresUsersService : IUsersService
{

	private readonly PostgresContext _context;

	public PostgresUsersService(PostgresContext context)
	{
		_context = context;
	}

	public async Task<User> CreateUSerAsync(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
		return user;

	}

	public async Task<User> GetUserByIdAsync(Guid id)
	{
		var user = await _context.Users.FindAsync(id);
		return user;
	}

	public async Task<User> GetUserByEmailAsync(string Email)
	{
		var user = await _context.Users.SingleOrDefaultAsync(_ => _.Email == Email);

		return user;
	}
}
