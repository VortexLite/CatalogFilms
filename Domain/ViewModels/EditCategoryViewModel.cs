namespace CatalogFilms.Models;

public class EditCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public string? NameParent { get; set; }
}