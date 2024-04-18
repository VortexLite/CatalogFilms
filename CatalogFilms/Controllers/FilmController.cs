using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.DAL;

namespace CatalogFilms.Controllers;

public class FilmController : Controller
{
    Uri _baseAdress = new Uri("http://localhost:5099/api");
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _db;

    public FilmController(ApplicationDbContext db)
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAdress;
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var films = new List<Films>();
        var response = await _client.GetAsync(_client.BaseAddress + "/Film/Get");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            films = JsonConvert.DeserializeObject<List<Films>>(data);
            
        }
        return View(films);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}