using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.IdentityModel.Tokens;
using mvcApp.Services;

namespace mvcApp.middleware;

public class IsLoggedInMiddleware : IMiddleware
{
	private readonly IConfiguration _configuration;
	private readonly IUsersService _service;

	public IsLoggedInMiddleware(IConfiguration configuration, IUsersService service)
	{
		_configuration = configuration;
		_service = service;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		//get TempData handle
		ITempDataDictionaryFactory factory = context.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
		ITempDataDictionary tempData = factory.GetTempData(context);

		var token = context.Request.Cookies["jwt"];

		if (token is null)
		{
			await next(context);
			return;
		}


		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

		tokenHandler.ValidateToken(token, new TokenValidationParameters()
		{
			ValidateAudience = false,
			ValidateIssuer = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key)
		}, out SecurityToken validatedToken);

		var jwtToken = (JwtSecurityToken)validatedToken;
		var Id = jwtToken.Claims.First(claim => claim.Type == "Id").Value;


		var user = await _service.GetUserByIdAsync(Guid.Parse(Id));

		if (user is null)
		{
			await next(context);
			return;
		}

		tempData["user"] = JsonSerializer.Serialize(user);
		await next(context);
		return;
	}
}