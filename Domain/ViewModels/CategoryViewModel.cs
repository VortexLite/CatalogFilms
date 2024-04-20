using Domain.Entity;

namespace CatalogFilms.Models;

public class CategoryViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int FilmCount { get; set; } 

    public int NestingLevel { get; set; }
}