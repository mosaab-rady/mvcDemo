using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcApp.Models;
using mvcApp.Services;
using Npgsql;
using razorWebApp.Utils;

namespace mvcApp.Controllers;

public class HomeController : Controller
{

	private readonly IProductsService _service;
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger, IProductsService service)
	{
		_logger = logger;
		_service = service;
	}

	public async Task<IActionResult> Index()
	{
		var products = await _service.GetAllProductsAsync();
		return View(products);
	}


	public async Task<IActionResult> Product(Guid id)
	{
		var product = await _service.GetProductByIdAsync(id);
		if (product is null)
		{
			throw new AppException("No Product found with that ID.", 404);
		}
		return View(product);
	}

	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(CreateProduct product)
	{
		if (!ModelState.IsValid)
		{
			return View(product);
		}

		var NewProduct = new Product()
		{
			Name = product.Name,
			Price = product.Price,
			Type = product.Type,
			Summary = product.Summary
		};

		try
		{
			await _service.CreateProductAsync(NewProduct);
		}
		catch (DbUpdateException err)
		{
			var error = (PostgresException?)err.InnerException;
			if (error?.SqlState == "23505")
			{
				ModelState.AddModelError("Name", "A product with that name already exist. Please use another value");
				return View(product);
			}
		}
		return RedirectToAction("Index");
	}



	public async Task<IActionResult> Edit(Guid id)
	{
		var product = await _service.GetProductByIdAsync(id);
		if (product is null)
		{
			throw new AppException("No Product found with that ID.", 404);
		}
		return View(product);
	}

	[HttpPost]
	public async Task<IActionResult> Edit([FromRoute] Guid id, Product product)
	{
		if (!ModelState.IsValid)
		{
			return View(product);
		}

		var existingProduct = await _service.GetProductByIdAsync(id);

		if (existingProduct is null)
		{
			throw new AppException("No Product found with that ID.", 404);
		}

		existingProduct.Name = product.Name ?? existingProduct.Name;
		existingProduct.Price = product.Price;
		existingProduct.Type = product.Type ?? existingProduct.Type;
		existingProduct.Summary = product.Summary ?? existingProduct.Summary;

		try
		{
			await _service.UpdateProductByIdAsync(id, existingProduct);
		}
		catch (DbUpdateException err)
		{
			var error = (PostgresException?)err.InnerException;
			if (error?.SqlState == "23505")
			{
				ModelState.AddModelError("Name", "A product with that name already exist. Please use another value");
				return View(existingProduct);
			}
		}

		return RedirectToAction("Product", new { id = existingProduct.Id });
	}



	public async Task<IActionResult> Delete(Guid id)
	{
		var product = await _service.GetProductByIdAsync(id);
		if (product is null)
		{
			throw new AppException("No product found with that ID.", 404);
		}
		return View(product);
	}

	[HttpPost, ActionName("Delete")]
	public async Task<IActionResult> DeletePost(Guid id)
	{
		var product = await _service.GetProductByIdAsync(id);
		if (product is null)
		{
			throw new AppException("No product found with that ID.", 404);
		}
		await _service.DeleteProductByIdAsync(id);
		return RedirectToAction("Index");
	}


	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{

		var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

		if (exceptionHandlerPathFeature?.Error is AppException)
		{
			AppException err = (AppException)exceptionHandlerPathFeature.Error;
			HttpContext.Response.StatusCode = err.statusCode;
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = err.Message });
		}
		else
		{
			HttpContext.Response.StatusCode = 500;
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Something went wrong, Please try later!!." });
		}
	}

	[HttpPost, ActionName("Error")]
	public IActionResult ErrorPost()
	{

		var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

		if (exceptionHandlerPathFeature?.Error is AppException)
		{
			AppException err = (AppException)exceptionHandlerPathFeature.Error;
			HttpContext.Response.StatusCode = err.statusCode;
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = err.Message });
		}
		else
		{
			HttpContext.Response.StatusCode = 500;
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Something went wrong, Please try later!!." });
		}
	}


}
