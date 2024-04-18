using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL;

namespace CatalogFilms.Controllers;

public class CategoryController : Controller
{
    Uri _baseAdress = new Uri("http://localhost:5099/api");
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAdress;
        _db = db;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}