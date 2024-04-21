using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Configurations;

public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(k => k.Id)
            .HasColumnName("CategoryId")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnType("varchar(200)")
            .IsRequired();
    }
}