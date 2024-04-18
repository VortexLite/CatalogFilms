using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;

namespace WebAPI.Controllers;

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
            await _db.SaveChangesAsync();
            return Ok("Film details deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}