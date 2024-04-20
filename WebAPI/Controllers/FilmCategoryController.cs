using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FilmCategoryController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public FilmCategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var filmCategories = await _db.FilmCategories
                .ToListAsync();
            if (filmCategories.Count == 0)
            {
                return NotFound("Films not available.");
            }

            return Ok(filmCategories);
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
            var filmCategories = await _db.FilmCategories.FirstOrDefaultAsync(i => i.Id == id);
            if (filmCategories == null)
            {
                return NotFound("Film not found.");
            }

            return Ok(filmCategories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(FilmCategories model)
    {
        await _db.AddAsync(model);
        await _db.SaveChangesAsync();
        return Ok("FilmCategories created.");
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(FilmCategories model)
    {
        if (model == null || model.Id == 0)
        {
            if (model == null)
            {
                return BadRequest("Model data is invalid");
            }
            else if (model.Id == 0)
            {
                return BadRequest($"FilmCategories Id {model.Id} is invalid");
            }
        }
        try
        {
            var filmCategories = await _db.FilmCategories.FindAsync(model.Id);
            if (filmCategories == null)
            {
                return NotFound($"FilmCategories not found with id {model.Id}");
            }

            filmCategories.FilmId = model.FilmId;
            filmCategories.CategoryId = model.CategoryId;

            await _db.SaveChangesAsync();
            return Ok("FilmCategories details updated");
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
            var filmCategories = await _db.FilmCategories.FindAsync(id);
            if (filmCategories == null)
            {
                return NotFound($"FilmCategories not found with id {id}");
            }
            
            _db.FilmCategories.Remove(filmCategories);
            await _db.SaveChangesAsync();
            return Ok("Film details deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}