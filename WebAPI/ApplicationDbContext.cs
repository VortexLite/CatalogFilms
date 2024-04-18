using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        /*Database.EnsureDeleted();
        Database.EnsureCreated();*/
    }
    
    public DbSet<Films> Films { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<FilmCategories> FilmCategories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}