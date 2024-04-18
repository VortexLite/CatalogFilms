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
    public async Task<IActionResult> Post(Films model)
    {
        await _db.AddAsync(model);
        await _db.SaveChangesAsync();
        return Ok("Film created.");
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(Films model)
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
            //await _db.SaveChangesAsync();
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