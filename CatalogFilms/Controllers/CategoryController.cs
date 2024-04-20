using CatalogFilms.Models;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatalogFilms.Controllers;

public class CategoryController : Controller
{
    Uri _baseAdress = new Uri("http://localhost:5099/api");
    private readonly HttpClient _client;

    public CategoryController()
    {
        _client = new HttpClient();
        _client.BaseAddress = _baseAdress;
    }
    
    public async Task<IActionResult> Index()
    {
        var categoryViewModels = new List<CategoryViewModel>();
        var response = await _client.GetAsync(_client.BaseAddress + "/Category/GetViewModel");
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            categoryViewModels = JsonConvert.DeserializeObject<List<CategoryViewModel>>(data);
        }
        return View(categoryViewModels);
    }

    public async Task<IActionResult> Create()
    {
        var responseCategory = await _client.GetAsync(_client.BaseAddress + "/Category/Get");
        if (responseCategory.IsSuccessStatusCode)
        {
            string data = await responseCategory.Content.ReadAsStringAsync();
            ViewBag.Categories = JsonConvert.DeserializeObject<List<Categories>>(data);
        }
        return View();
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var curentCategory = new EditCategoryViewModel();
        var responseCurrentCategory = await _client.GetAsync(_client.BaseAddress + $"/Category/GetEditViewModel/{id}");
        
        var responseCategories = await _client.GetAsync(_client.BaseAddress + "/Category/Get");
        if (responseCategories.IsSuccessStatusCode && responseCurrentCategory.IsSuccessStatusCode)
        {
            string data = await responseCategories.Content.ReadAsStringAsync();
            ViewBag.Categories = JsonConvert.DeserializeObject<List<Categories>>(data);
            
            string data2 = await responseCurrentCategory.Content.ReadAsStringAsync();
            curentCategory = JsonConvert.DeserializeObject<EditCategoryViewModel>(data2);
        }
        return View(curentCategory);
    }
}