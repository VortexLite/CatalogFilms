namespace WebAPI.Models;

public class Films
{
    public int FilmId { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }

    public List<FilmCategories> FilmCategories { get; set; }
}