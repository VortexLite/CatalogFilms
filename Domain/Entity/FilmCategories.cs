namespace WebAPI.Models;

public class FilmCategories
{
    public int FilmCategoryId { get; set; }
    
    public int FilmId { get; set; }
    public Films Films { get; set; }
    
    public int CategoryId { get; set; }
    public Categories Categories { get; set; }

    
}