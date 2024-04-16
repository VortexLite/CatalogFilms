using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.DAL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.HasKey(k => k.CategoryId);

        builder.Property(k => k.CategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnType("varchar(200)");
    }
}