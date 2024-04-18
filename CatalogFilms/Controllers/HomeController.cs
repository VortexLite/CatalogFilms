using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CatalogFilms.Models;
using Newtonsoft.Json;
using WebAPI.DAL;

namespace CatalogFilms.Controllers;

public class HomeController : Controller
{
    Uri _baseAdress = new Uri("http://localhost:5099/api");
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _client = new HttpClient();
        _client.BaseAddress = _baseAdress;
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var films = new List<FilmWithCategoriesViewModel>();
        var response = await _client.GetAsync(_client.BaseAddress + "/Film/GetFilmCategory");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            films = JsonConvert.DeserializeObject<List<FilmWithCategoriesViewModel>>(data);
        }
        return View(films);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}