using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.DAL.Configurations;

public class FilmCategoriesConfiguration : IEntityTypeConfiguration<FilmCategories>
{
    public void Configure(EntityTypeBuilder<FilmCategories> builder)
    {
        builder.HasKey(k => k.FilmCategoryId);

        builder.Property(k => k.FilmCategoryId)
            .ValueGeneratedOnAdd();

        builder.HasOne(fc => fc.Films)
            .WithMany(f => f.FilmCategories)
            .HasForeignKey(fc => fc.FilmId);
        
        builder.HasOne(fc => fc.Categories)
            .WithMany(c => c.FilmCategories)
            .HasForeignKey(fc => fc.CategoryId);
    }
}