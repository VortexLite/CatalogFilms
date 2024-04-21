using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class Films
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateTime Release { get; set; }

    public List<FilmCategories>? FilmCategories { get; set; }
}