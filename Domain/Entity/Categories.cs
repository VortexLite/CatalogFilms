using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class Categories
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CategoryId")]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    public int? ParentCategoryId { get; set; }

    public List<FilmCategories>? FilmCategories { get; set; }
}