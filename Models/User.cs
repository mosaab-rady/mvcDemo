using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace mvcApp.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
	public Guid Id { get; set; }
	[Required]
	public string FirstName { get; set; }
	[Required]
	public string LastName { get; set; }
	[Required]
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
	public string Role { get; set; } = "user";
}

public class SignUpModel
{
	[Required(ErrorMessage = "Please provide your first name.")]
	[DisplayName("First Name")]
	public string FirstName { get; set; }


	[Required(ErrorMessage = "Please provide your last name.")]
	[DisplayName("Last Name")]
	public string LastName { get; set; }


	[Required(ErrorMessage = "Please provide your email.")]
	[EmailAddress(ErrorMessage = "Please provide valid email address.")]
	public string Email { get; set; }


	[Required(ErrorMessage = "Please provide your password.")]
	public string Password { get; set; }


	[Required(ErrorMessage = "Please confim your password.")]
	[Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
	[DisplayName("Confirm Password")]
	public string ConfirmPassword { get; set; }
}

public class LoginModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	public string Password { get; set; }
}