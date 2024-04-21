using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPI.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Films>
{
    public void Configure(EntityTypeBuilder<Films> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(k => k.Id)
            .HasColumnName("FilmId")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(p => p.Director)
            .HasColumnType("varchar(200)")
            .IsRequired();
        
        builder.Property(p => p.Release)
            .IsRequired();
    }
}