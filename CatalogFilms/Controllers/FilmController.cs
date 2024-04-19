using System.Text;
using CatalogFilms.Models;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogFilms.Controllers;

public class FilmController : Controller
{
    private readonly Uri _baseAdress = new Uri("http://localhost:5099/api");
    private readonly HttpClient _client;

    public FilmController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAdress;
    }
    
    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = new List<Categories>();
        var response = await _client.GetAsync(_client.BaseAddress + "/Category/Get");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            ViewBag.Categories = JsonConvert.DeserializeObject<List<Categories>>(data);
        }
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(FilmWithCategoriesViewModel model)
    {
        try
        {
            string data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Film/Post", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Film created.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception e)
        {
            TempData["errorMessage"] = e.Message;
            return View();
        }

        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var film = new EditViewModel();
            var response = await _client.GetAsync(_client.BaseAddress + $"/Film/GetEditFilm/{id}");
            
            var categories = new List<Categories>();
            var responseCategory = await _client.GetAsync(_client.BaseAddress + "/Category/Get");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                film = JsonConvert.DeserializeObject<EditViewModel>(data);

                string data2 = await responseCategory.Content.ReadAsStringAsync();
                ViewBag.Categories = JsonConvert.DeserializeObject<List<Categories>>(data2);
            }
            return View(film);
        }
        catch (Exception e)
        {
            TempData["errorMessage"] = e.Message;
            return View();
        }
    }

    /*[HttpPost]
    public async Task<IActionResult> Edit(EditViewModel model)
    {
        try
        {
            string data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_client.BaseAddress + "/Film/Put", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Film details updated";
                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception e)
        {
            TempData["errorMessage"] = e.Message;
            return View();
        }
    }*/
}