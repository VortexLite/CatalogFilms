using CatalogFilms.Models;
using Domain.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;

namespace WebAPI.Controllers;

[EnableCors("AllowSpecificOrigin")]
[ApiController]
[Route("api/[controller]/[action]")]
public class FilmController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public FilmController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var films = await _db.Films.ToListAsync();
            if (films.Count == 0)
            {
                return NotFound("Films not available.");
            }

            return Ok(films);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var film = await _db.Films.FirstOrDefaultAsync(i => i.Id == id);
            if (film == null)
            {
                return NotFound("Film not found.");
            }

            return Ok(film);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(FilmWithCategoriesViewModel model)
    {
        if (ModelState.IsValid)
        {
            var film = new Films()
            {
                Name = model.Name,
                Director = model.Director,
                Release = model.Release
            };
            
            await _db.Films.AddAsync(film);
            await _db.SaveChangesAsync();

            if (model.SelectedCategoriesList != null)
            {
                foreach (var categoryId in model.SelectedCategoriesList)
                {
                    var filmCategory = new FilmCategories
                    {
                        FilmId = film.Id,
                        CategoryId = categoryId
                    };
                    await _db.FilmCategories.AddAsync(filmCategory);
                    
                }
                await _db.SaveChangesAsync();
            }
            
            return Ok("Film created.");
        }
        return BadRequest("Invalid model state.");
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(FilmWithCategoriesViewModel model)
    {
        if (model == null || model.Id == 0)
        {
            if (model == null)
            {
                return BadRequest("Model data is invalid");
            }
            else if (model.Id == 0)
            {
                return BadRequest($"Film Id {model.Id} is invalid");
            }
        }

        try
        {
            var film = await _db.Films.FindAsync(model.Id);
            if (film == null)
            {
                return NotFound($"Film not found with id {model.Id}");
            }

            film.Name = model.Name;
            film.Director = model.Director;
            film.Release = model.Release;
            
            // Видаляємо всі категорії фільму
            var existingCategories = await _db.FilmCategories.Where(fc => fc.FilmId == film.Id).ToListAsync();
            _db.FilmCategories.RemoveRange(existingCategories);

            // Додаємо вибрані категорії
            if (model.SelectedCategoriesList != null)
            {
                foreach (var categoryId in model.SelectedCategoriesList)
                {
                    var filmCategory = new FilmCategories
                    {
                        FilmId = film.Id,
                        CategoryId = categoryId
                    };
                    await _db.FilmCategories.AddAsync(filmCategory);
                }
            }
            
            await _db.SaveChangesAsync();
            return Ok("Film details updated");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var film = await _db.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound($"Film not found with id {id}");
            }
            
            _db.Films.Remove(film);
            await _db.SaveChangesAsync();
            return Ok("Film details deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFilmCategory()
    {
        try
        {
            var filmsWithCategories = await _db.Films
                .Select(film => new FilmWithCategoriesViewModel
                {
                    Id = film.Id,
                    Name = film.Name,
                    Director = film.Director,
                    Release = film.Release,
                    CategoriesList = film.FilmCategories.Select(fc => new Categories()
                    {
                        Id = fc.Category.Id,
                        Name = fc.Category.Name
                    }).ToList()
                })
                .ToListAsync();
            
            if (filmsWithCategories == null)
            {
                return NotFound($"FilmWithCategoriesViewModel not found");
            }

            return Ok(filmsWithCategories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFilmCategoryId(int id)
    {
        try
        {
            var filmsWithCategories = await _db.Films
                .Where(i => i.Id == id)
                .Select(film => new FilmWithCategoriesViewModel
                {
                    Id = film.Id,
                    Name = film.Name,
                    Director = film.Director,
                    Release = film.Release,
                    CategoriesList = film.FilmCategories.Select(fc => new Categories()
                    {
                        Id = fc.Category.Id,
                        Name = fc.Category.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            
            if (filmsWithCategories == null)
            {
                return NotFound($"FilmWithCategoriesViewModel not found");
            }

            return Ok(filmsWithCategories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEditFilm(int id)
    {
        try
        {
            var film = await _db.Films
                .Where(i => i.Id == id)
                .Select(film => new EditViewModel()
                {
                    Id = film.Id,
                    Name = film.Name,
                    Director = film.Director,
                    Release = film.Release,
                    CategoriesList = film.FilmCategories.Select(fc => new Categories()
                    {
                        Id = fc.Category.Id,
                        Name = fc.Category.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            
            if (film == null)
            {
                return NotFound($"EditViewModel not found");
            }

            return Ok(film);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Filter(string? sortOrder, string? directorName, string? categoryFilter)
    {
        try
        {
            var query = _db.Films.AsQueryable();
            
            if (!string.IsNullOrEmpty(directorName))
            {
                query = query.Where(f => EF.Functions.Like(f.Director, $"%{directorName}%"));
            }
            
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                query = query.Where(f => f.FilmCategories.Any(fc => fc.Category.Name == categoryFilter));
            }
            
            switch (sortOrder)
            {
                case "asc":
                    query = query.OrderBy(f => f.Release);
                    break;
                case "desc":
                    query = query.OrderByDescending(f => f.Release);
                    break;
                default:
                    break;
            }

            var films = await query
                .Select(film => new FilmWithCategoriesViewModel
                {
                    Id = film.Id,
                    Name = film.Name,
                    Director = film.Director,
                    Release = film.Release,
                    CategoriesList = film.FilmCategories.Select(fc => new Categories
                    {
                        Id = fc.Category.Id,
                        Name = fc.Category.Name
                    }).ToList()
                })
                .ToListAsync();

            if (films.Count == 0)
            {
                return NotFound("Films not found.");
            }

            return Ok(films);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}