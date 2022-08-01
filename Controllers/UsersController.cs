using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mvcApp.Models;
using mvcApp.Services;
using Npgsql;

namespace mvcApp.Controllers;

public class UsersController : Controller
{
	private readonly IUsersService _service;
	private readonly IConfiguration configuration;

	public UsersController(IUsersService service, IConfiguration configuration)
	{
		_service = service;
		this.configuration = configuration;
	}

	public IActionResult Signup()
	{
		return View();
	}

	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Signup(SignUpModel user)
	{
		if (!ModelState.IsValid)
		{
			return View(user);
		}
		var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

		var _user = new User
		{
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			Password = hashedPassword
		};

		try
		{
			var newUser = await _service.CreateUSerAsync(_user);
			var token = createToken(newUser);
			TempData["success"] = $"Signed UP Successfully";
		}
		catch (DbUpdateException err)
		{
			var error = (PostgresException)err.InnerException;
			if (error?.SqlState == "23505")
			{
				ModelState.AddModelError("Email", "A User with that Email already exist!. Please Use another Email address.");
				return View(user);
			}
		}


		return RedirectToAction("Index", "Home");
	}


	[HttpPost]
	public async Task<IActionResult> Login(LoginModel _user)
	{
		if (!ModelState.IsValid)
		{
			return View(_user);
		}
		var user = await _service.GetUserByEmailAsync(_user.Email);

		if (user == null || !BCrypt.Net.BCrypt.Verify(_user.Password, user.Password))
		{
			ModelState.AddModelError("Email", "Incorrect Email OR Password.");
			return View(_user);
		}

		var token = createToken(user);
		TempData["success"] = $"Logged In Successfully";
		return RedirectToAction("Index", "Home");

	}

	[HttpPost]
	public IActionResult Logout()
	{
		HttpContext.Response.Cookies.Delete("jwt");
		TempData["danger"] = "Logged out!!";
		TempData["user"] = null;
		return RedirectToAction("Index", "Home");
	}

	private string createToken(User user)
	{
		var claims = new[]
						{
						new Claim("Id",user.Id.ToString()),
				};


		var token = new JwtSecurityToken
						 (
								 claims: claims,
								 expires: DateTime.UtcNow.AddDays(60),
								 notBefore: DateTime.UtcNow,
								 signingCredentials: new SigningCredentials(
										 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
										 SecurityAlgorithms.HmacSha256)
						 );

		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);



		var cookieOptions = new CookieOptions()
		{
			HttpOnly = true,
			Expires = DateTimeOffset.Now.AddHours(1)
		};


		Response.Cookies.Append("jwt", tokenString, cookieOptions);

		return tokenString;
	}
}