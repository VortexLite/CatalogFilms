using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class Films
{
    [Key]
    [Column("FilmId")]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Director { get; set; }

    [Required]
    public DateTime Release { get; set; }

    public List<FilmCategories> FilmCategories { get; set; }
}