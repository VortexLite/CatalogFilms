using Domain.Entity;

namespace WebAPI;

public class DbInitializer
{
    private readonly ApplicationDbContext _db;
    public DbInitializer(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task SeedCategories()
    {
        if (!_db.Categories.Any())
        {
            var categories = new List<Categories>()
            {
                new Categories()
                {
                    Name = "Драма"
                },
                new Categories()
                {
                    Name = "Комедія"
                },
                new Categories()
                {
                    Name = "Екшн"
                },
                new Categories()
                {
                    Name = "Жахи"
                },
                new Categories()
                {
                    Name = "Наукова фантастика"
                },
                new Categories()
                {
                    Name = "Сімейна драма",
                    ParentCategoryId = 1
                },
                new Categories()
                {
                    Name = "Психологічна драма",
                    ParentCategoryId = 1
                },
                new Categories()
                {
                    Name = "Історична драма",
                    ParentCategoryId = 1
                },
                new Categories()
                {
                    Name = "Ситком",
                    ParentCategoryId = 2
                },
                new Categories()
                {
                    Name = "Романтична комедія",
                    ParentCategoryId = 2
                },
                new Categories()
                {
                    Name = "Чорна комедія",
                    ParentCategoryId = 2
                },
                new Categories()
                {
                    Name = "Пригодницький екшн",
                    ParentCategoryId = 3
                },
                new Categories()
                {
                    Name = "Бойовик",
                    ParentCategoryId = 3
                },
                new Categories()
                {
                    Name = "Супергеройський фільм",
                    ParentCategoryId = 3
                },
                new Categories()
                {
                    Name = "Слешер",
                    ParentCategoryId = 4
                },
                new Categories()
                {
                    Name = "Психологічний жах",
                    ParentCategoryId = 4
                },
                new Categories()
                {
                    Name = "Надприродний жах",
                    ParentCategoryId = 4
                },
                new Categories()
                {
                    Name = "Космічна наукова фантастика",
                    ParentCategoryId = 5
                },
                new Categories()
                {
                    Name = "Постапокаліптична драма",
                    ParentCategoryId = 5
                },
                new Categories()
                {
                    Name = "Кіберпанк",
                    ParentCategoryId = 5
                }
            };
            await _db.AddRangeAsync(categories);
            await _db.SaveChangesAsync();
        }
    }
    
    public async Task SeedFilms()
    {
        if (!_db.Films.Any())
        {
            var films = new List<Films>()
            {
                new Films()
                {
                    Name = "Forrest Gump",
                    Director = "Роберт Земекіс",
                    Release = new DateTime(1991, 1, 1)
                },
                new Films()
                {
                    Name = "Schindler's List",
                    Director = "Стівен Спілберг",
                    Release = new DateTime(1993, 1, 1)
                },
                new Films()
                {
                    Name = "The Shawshank Redemption",
                    Director = "Френк Дарабонт",
                    Release = new DateTime(1994, 1, 1)
                },
                new Films()
                {
                    Name = "The Hangover",
                    Director = "Тодд Філліпс",
                    Release = new DateTime(2009, 1, 1)
                },
                new Films()
                {
                    Name = "Superbad",
                    Director = "Ґреґ Моттола",
                    Release = new DateTime(2007, 1, 1)
                },
                new Films()
                {
                    Name = "Bridesmaids",
                    Director = "Пол Фейґ",
                    Release = new DateTime(2011, 1, 1)
                },
                new Films()
                {
                    Name = "The Dark Knight",
                    Director = "Крістофер Нолан",
                    Release = new DateTime(2008, 1, 1)
                },
                new Films()
                {
                    Name = "Inception",
                    Director = "Крістофер Нолан",
                    Release = new DateTime(2010, 1, 1)
                },
                new Films()
                {
                    Name = "Mad Max: Fury Road ",
                    Director = "Джордж Міллер",
                    Release = new DateTime(2015, 1, 1)
                },
                new Films()
                {
                    Name = "The Exorcist",
                    Director = "Вільям Фрідкін",
                    Release = new DateTime(1973)
                },
                new Films()
                {
                    Name = "The Shining",
                    Director = "Стенлі Кубрик",
                    Release = new DateTime(1980, 1, 1)
                },
                new Films()
                {
                    Name = "Psycho",
                    Director = "Альфред Хічкок",
                    Release = new DateTime(1960, 1, 1)
                },
                new Films()
                {
                    Name = "Blade Runner",
                    Director = "Рідлі Скотт",
                    Release = new DateTime(1982, 1, 1)
                },
                new Films()
                {
                    Name = "Interstellar",
                    Director = "Крістофер Нолан",
                    Release = new DateTime(2014, 1, 1)
                },
                new Films()
                {
                    Name = "The Terminator",
                    Director = "Джеймс Кемерон",
                    Release = new DateTime(1984, 1, 1)
                }
            };
            await _db.AddRangeAsync(films);
            await _db.SaveChangesAsync();
        }
    }
    
    public async Task SeedFilmsCategories()
    {
        if (!_db.FilmCategories.Any())
        {
            var filmsCategories = new List<FilmCategories>()
            {
                new FilmCategories()
                {
                    FilmId = 1,
                    CategoryId = 6
                },
                new FilmCategories()
                {
                    FilmId = 2,
                    CategoryId = 7
                },
                new FilmCategories()
                {
                    FilmId = 3,
                    CategoryId = 8
                },
                new FilmCategories()
                {
                    FilmId = 4,
                    CategoryId = 9
                },
                new FilmCategories()
                {
                    FilmId = 5,
                    CategoryId = 10
                },
                new FilmCategories()
                {
                    FilmId = 6,
                    CategoryId = 11
                },
                new FilmCategories()
                {
                    FilmId = 7,
                    CategoryId = 12
                },
                new FilmCategories()
                {
                    FilmId = 8,
                    CategoryId = 13
                },
                new FilmCategories()
                {
                    FilmId = 9,
                    CategoryId = 14
                },
                new FilmCategories()
                {
                    FilmId = 10,
                    CategoryId = 15
                },
                new FilmCategories()
                {
                    FilmId = 11,
                    CategoryId = 16
                },
                new FilmCategories()
                {
                    FilmId = 12,
                    CategoryId = 17
                },
                new FilmCategories()
                {
                    FilmId = 13,
                    CategoryId = 18
                },
                new FilmCategories()
                {
                    FilmId = 14,
                    CategoryId = 19
                },
                new FilmCategories()
                {
                    FilmId = 15,
                    CategoryId = 20
                }
            };
            await _db.AddRangeAsync(filmsCategories);
            await _db.SaveChangesAsync();
        }
    }
}