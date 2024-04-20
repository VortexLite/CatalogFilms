using CatalogFilms.Models;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return NotFound("Category not available.");
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
                return NotFound("Category not found.");
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
        try
        {
            var parentCategory = await _db.Categories.FindAsync(model.ParentCategoryId);
            if (await IsCircularReference(model.Id, parentCategory))
            {
                return BadRequest("This will lead to a cyclic dependency of the categories.");
            }

            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return Ok("Category created.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(Categories model)
    {
        if (model.ParentCategoryId.HasValue)
        {
            var parentCategory = await _db.Categories.FindAsync(model.ParentCategoryId);
            if (await IsCircularReference(model.Id, parentCategory))
            {
                return BadRequest("This will lead to a cyclical dependence of categories.");
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
    private async Task<bool> IsCircularReference(int categoryId, Categories parentCategory)
    {
        if (parentCategory == null)
        {
            return false;
        }

        if (parentCategory.Id == categoryId)
        {
            return true;
        }

        return await IsCircularReference(categoryId, parentCategory.ParentCategoryId.HasValue ? 
            await _db.Categories.FindAsync(parentCategory.ParentCategoryId.Value) : 
            null);
    }

    [HttpGet]
    public async Task<IActionResult> GetViewModel()
    {
        try
        {
            var categoryViewModels = new List<CategoryViewModel>();
            var categories = await _db.Categories.ToListAsync();
            if (categories.Count == 0)
            {
                return NotFound("Categories not available.");
            }
            
            foreach (var category in categories)
            {
                var categoryViewModel = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    FilmCount = await GetFilmCountAsync(category.Id),
                    NestingLevel = await GetNestingLevelAsync(category.Id) - 1
                };

                categoryViewModels.Add(categoryViewModel);
            }
            
            return Ok(categoryViewModels);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEditViewModel(int id)
    {
        try
        {
            var categories = await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (categories == null)
            {
                return NotFound("Category not found.");
            }
            
            var parentCategories = await _db.Categories.FirstOrDefaultAsync(i => i.Id == categories.ParentCategoryId);
            if (parentCategories == null)
            {
                return NotFound("Category not found.");
            }
            
            var editCategoryViewModel = new EditCategoryViewModel
            {
                Id = categories.Id,
                Name = categories.Name,
                ParentCategoryId = categories.ParentCategoryId,
                NameParent = parentCategories.Name
            };

            return Ok(editCategoryViewModel);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    private async Task<int> GetFilmCountAsync(int categoryId)
    {
        return await _db.FilmCategories.CountAsync(fc => fc.CategoryId == categoryId);
    }
    
    private async Task<int> GetNestingLevelAsync(int categoryId)
    {
        var nestingLevel = 0;
        int? currentCategoryId = categoryId;

        while (currentCategoryId != null)
        {
            var parentCategory = await _db.Categories.FirstOrDefaultAsync(c => c.Id == currentCategoryId);
            currentCategoryId = parentCategory?.ParentCategoryId;
            nestingLevel++;
        }

        return nestingLevel;
    }
}