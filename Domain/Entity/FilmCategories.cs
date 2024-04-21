using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class FilmCategories
{
    public int Id { get; set; }
    
    public int FilmId { get; set; }
    public Films Film { get; set; }
    
    public int CategoryId { get; set; }
    public Categories Category { get; set; }
}