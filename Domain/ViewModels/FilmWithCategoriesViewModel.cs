using Domain.Entity;

namespace CatalogFilms.Models;

public class FilmWithCategoriesViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }
    public List<Categories> CategoriesList { get; set; }
}