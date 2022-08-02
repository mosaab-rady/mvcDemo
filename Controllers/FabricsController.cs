using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcApp.Models;
using mvcApp.Services;
using Npgsql;

namespace mvcApp.Controllers;

public class FabricsController : Controller
{
	private readonly IFabricService _service;

	public FabricsController(IFabricService service)
	{
		_service = service;
	}

	public async Task<IActionResult> Index()
	{
		var fabrics = await _service.GetFabricsAsync();
		return View(fabrics);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(Fabric fabric)
	{
		if (!ModelState.IsValid)
		{
			return View(fabric);
		}

		try
		{

			await _service.CreateFabricAsync(fabric);
		}
		catch (DbUpdateException err)
		{
			var error = (PostgresException)err.InnerException;
			if (error?.SqlState == "23505")
			{
				ModelState.AddModelError("Name", "A fabric with that name already exist. Please use another value");
				return View(fabric);
			}
		}
		TempData["success"] = "Created Fabric Successfully";
		return RedirectToAction("Index", "Fabrics");
	}
}