using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.DAL.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Films>
{
    public void Configure(EntityTypeBuilder<Films> builder)
    {
        builder.HasKey(k => k.FilmId);

        builder.Property(k => k.FilmId)
            .ValueGeneratedOnAdd();

        builder.Property(n => n.Name)
            .HasColumnType("varchar(200)");

        builder.Property(d => d.Director)
            .HasColumnType("varchar(200)");

        builder.Property(r => r.Release)
            .HasColumnType("datetime");
    }
}