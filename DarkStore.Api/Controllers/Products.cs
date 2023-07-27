using Microsoft.AspNetCore.Mvc;

namespace DarkStore.Api.Controllers;

public class Products : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}