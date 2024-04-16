namespace WebAPI.Models;

public class Categories
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }

    public List<FilmCategories> FilmCategories { get; set; }
}