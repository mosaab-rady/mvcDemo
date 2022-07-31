using mvcApp.Models;

namespace mvcApp.Services;

public interface IUsersService
{
	Task<User> CreateUSerAsync(User user);
	Task<User?> GetUserByEmailAsync(string? Email);
}