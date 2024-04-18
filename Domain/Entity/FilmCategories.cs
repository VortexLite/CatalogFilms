using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class FilmCategories
{
    [Key]
    [Column("FilmCategoryId")]
    public int Id { get; set; }

    [Required]
    public int FilmId { get; set; }
    [ForeignKey("FilmId")]
    public Films Film { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Categories Category { get; set; }

    
}