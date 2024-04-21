using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Configurations;

public class FilmCategoriesConfiguration : IEntityTypeConfiguration<FilmCategories>
{
    public void Configure(EntityTypeBuilder<FilmCategories> builder)
    {
        builder.HasKey(k => k.Id);
        
        builder.Property(k => k.Id)
            .HasColumnName("FilmCategoryId")
            .ValueGeneratedOnAdd();
        
        builder.HasOne(o => o.Film)
            .WithMany(u => u.FilmCategories)
            .HasForeignKey(o => o.FilmId);
        
        builder.HasOne(o => o.Category)
            .WithMany(u => u.FilmCategories)
            .HasForeignKey(o => o.CategoryId);
    }
}