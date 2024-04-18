using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categories = await _db.Categories.ToListAsync();
            if (categories.Count == 0)
            {
                return NotFound("Films not available.");
            }

            return Ok(categories);
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
            var categories = await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (categories == null)
            {
                return NotFound("Film not found.");
            }

            return Ok(categories);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Categories model)
    {
        await _db.AddAsync(model);
        await _db.SaveChangesAsync();
        return Ok("Categories created.");
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(Categories model)
    {
        if (model == null || model.Id == 0)
        {
            if (model == null)
            {
                return BadRequest("Model data is invalid");
            }
            else if (model.Id == 0)
            {
                return BadRequest($"Categories Id {model.Id} is invalid");
            }
        }

        try
        {
            var categories = await _db.Categories.FindAsync(model.Id);
            if (categories == null)
            {
                return NotFound($"Categories not found with id {model.Id}");
            }

            categories.Name = model.Name;
            categories.ParentCategoryId = model.ParentCategoryId;

            await _db.SaveChangesAsync();
            return Ok("Categories details updated");
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
            var categories = await _db.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound($"Categories not found with id {id}");
            }
            
            _db.Categories.Remove(categories);
            await _db.SaveChangesAsync();
            return Ok("Categories details deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    private bool CheckForCycle(int categoryId, int? parentCategoryId)
    {
        if (!parentCategoryId.HasValue)
            return false;

        var parentCategory = _db.Categories.Find(parentCategoryId);

        while (parentCategory != null)
        {
            if (parentCategory.Id == categoryId)
                return true;

            parentCategory = _db.Categories.Find(parentCategory.ParentCategoryId);
        }

        return false;
    }
}