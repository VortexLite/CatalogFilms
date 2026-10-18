# CatalogFilms 🎬


A C# ASP.NET Core Web API project designed for managing a catalog of films and their categories.

## Table of Contents 📄

- [Project Title & Badges](#project-title--badges)
- [Description](#description)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [API Reference](#api-reference)
- [Contributing](#contributing)
- [License](#license)
- [Important Links](#important-links)
- [Footer](#footer)

## Project Title & Badges 📛

# CatalogFilms

## Description 📝

CatalogFilms is a C# ASP.NET Core Web API application that provides a backend system for managing a film catalog. It allows for the creation, retrieval, update, and deletion of films and their associated categories. The application utilizes Entity Framework Core for data persistence, with an initial migration already in place.

## Features ✨

- **Film Management:** Create, read, update, and delete film records.
- **Category Management:** Create, read, update, and delete category records.
- **Film-Category Association:** Link films to multiple categories and manage these relationships.
- **Data Persistence:** Utilizes Entity Framework Core with a SQL Server database (inferred from `appsettings.json` and `DbInitializer.cs`).
- **API Endpoints:** Provides RESTful API endpoints for interacting with film and category data.
- **Client-Side Validation:** Integrates jQuery Validate unobtrusive client-side validation for forms.

## Tech Stack 💻

- **Language:** C#
- **Framework:** ASP.NET Core Web API
- **Database:** Entity Framework Core (SQL Server implied)
- **Frontend Libraries:** jQuery, jQuery Validate Unobtrusive, Bootstrap (inferred from `wwwroot` files)
- **Development Environment:** Visual Studio (inferred from `.idea` and `.csproj` files)

## Installation 🚀

1.  **Prerequisites:**
    *   .NET SDK (version compatible with ASP.NET Core 6.0 or later, as suggested by `Program.cs` and `WebAPI.csproj`)
    *   SQL Server instance (or any other compatible database provider if the connection string is modified).
    *   Visual Studio or another compatible IDE.

2.  **Clone the Repository:**
    ```bash
    git clone https://github.com/VortexLite/CatalogFilms.git
    cd CatalogFilms
    ```

3.  **Database Setup:**
    *   Open the `CatalogFilms.sln` file in Visual Studio.
    *   Update the connection string in `WebAPI/appsettings.json` and `WebAPI/appsettings.Development.json` to point to your SQL Server instance.
    *   Ensure you have the necessary Entity Framework Core tools installed:
        ```bash
        dotnet tool install --global dotnet-ef
        ```
    *   Apply the initial migration:
        ```bash
        cd WebAPI
        dotnet ef database update
        ```
        This will create the database and necessary tables based on the `DbInitializer.cs` and migration files.

4.  **Build and Run:**
    *   Restore NuGet packages:
        ```bash
        dotnet restore
        ```
    *   Build the project:
        ```bash
        dotnet build
        ```
    *   Run the Web API:
        ```bash
        dotnet run --project WebAPI/WebAPI.csproj
        ```
        The API will typically run on `http://localhost:5000` or `https://localhost:5001` (or similar ports defined in `WebAPI/Properties/launchSettings.json`).

## Usage 💡

This project is a Web API, intended to be consumed by a frontend application or other services.

**Example API Endpoints (based on `WebAPI/Controllers` and `wwwroot/js/Classes.js`):**

*   **Films:**
    *   `GET /api/Film/Get` - Retrieves all films.
    *   `GET /api/Film/Get/{id}` - Retrieves a specific film by ID.
    *   `POST /api/Film/Post` - Adds a new film.
    *   `PUT /api/Film/Put` - Updates an existing film.
    *   `DELETE /api/Film/Delete?id={id}` - Deletes a film by ID.
    *   `GET /api/Film/GetFilmCategory` - Retrieves film categories.
    *   `GET /api/Film/Filter` - Filters films based on sort order, director name, and category.

*   **Categories:**
    *   `GET /api/Category/Get` - Retrieves all categories.
    *   `GET /api/Category/Get/{id}` - Retrieves a specific category by ID.
    *   `POST /api/Category/Post` - Adds a new category.
    *   `PUT /api/Category/Put` - Updates an existing category.
    *   `DELETE /api/Category/Delete?id={id}` - Deletes a category by ID.

**Client-Side Interaction (example using `wwwroot/js/Classes.js`):**

If you were to build a frontend that consumes this API, you might use the provided JavaScript classes:

```javascript
// Example for FilmService
const filmService = new FilmService('api/Film'); // Assuming base URL is /api/Film

async function loadFilms() {
    try {
        const films = await filmService.getFilm();
        console.log('Films:', films);
        // Render films in UI
    } catch (error) {
        console.error('Error fetching films:', error);
    }
}

loadFilms();

async function addSampleFilm() {
    const newFilm = {
        Title: "Inception",
        Director: "Christopher Nolan",
        Year: 2010
        // ... other film properties
    };
    try {
        const result = await filmService.addFilm(newFilm);
        console.log('Add Film Result:', result);
    } catch (error) {
        console.error('Error adding film:', error);
    }
}
// addSampleFilm(); // Uncomment to add a sample film
```

## Project Structure 📂

```
CatalogFilms/
├── .idea/
│   ├── config/
│   │   └── applicationhost.config
│   └── .idea.CatalogFilms/
│       └── .idea/
│           ├── encodings.xml
│           └── material_theme_project_new.xml
├── CatalogFilms.sln
├── CatalogFilms/
│   ├── CatalogFilms.csproj
│   ├── CatalogFilms.csproj.DotSettings.user
│   ├── Controllers/
│   │   ├── CategoryController.cs
│   │   ├── FilmController.cs
│   │   └── HomeController.cs
│   ├── Models/
│   │   └── ErrorViewModel.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── Views/
│   │   ├── Category/
│   │   │   ├── Create.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── Index.cshtml
│   │   ├── Film/
│   │   │   ├── Create.cshtml
│   │   │   ├── Edit.cshtml
│   │   │   └── Index.cshtml
│   │   ├── Home/
│   │   │   ├── Index.cshtml
│   │   │   └── Privacy.cshtml
│   │   ├── Shared/
│   │   │   ├── Error.cshtml
│   │   │   ├── _Layout.cshtml
│   │   │   └── _Layout.cshtml.css
│   │   │   └── _ValidationScriptsPartial.cshtml
│   │   ├── _ViewImports.cshtml
│   │   └── _ViewStart.cshtml
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   └── wwwroot/
│       ├── css/
│       │   └── site.css
│       ├── js/
│       │   ├── Classes.js
│       │   └── site.js
│       └── lib/
│           ├── bootstrap/
│           │   └── LICENSE
│           ├── jquery/
│           │   └── LICENSE.txt
│           └── jquery-validation-unobtrusive/
│               ├── LICENSE.txt
│               └── jquery.validate.unobtrusive.js
│               └── jquery.validate.unobtrusive.min.js
├── Domain/
│   ├── Domain.csproj
│   ├── Entity/
│   │   ├── Categories.cs
│   │   ├── FilmCategories.cs
│   │   └── Films.cs
│   └── ViewModels/
│       ├── CategoryViewModel.cs
│       ├── EditCategoryViewModel.cs
│       ├── EditViewModel.cs
│       └── FilmWithCategoriesViewModel.cs
└── WebAPI/
    ├── ApplicationDbContext.cs
    ├── Configurations/
    │   ├── CategoriesConfiguration.cs
    │   ├── FilmCategoriesConfiguration.cs
    │   └── FilmConfiguration.cs
    ├── Controllers/
    │   ├── CategoryController.cs
    │   ├── FilmCategoryController.cs
    │   └── FilmController.cs
    ├── DbInitializer.cs
    ├── Migrations/
    │   ├── 20240421080024_Initial.Designer.cs
    │   ├── 20240421080024_Initial.cs
    │   └── ApplicationDbContextModelSnapshot.cs
    ├── Program.cs
    ├── Properties/
    │   └── launchSettings.json
    ├── WebAPI.csproj
    ├── WebAPI.http
    ├── appsettings.Development.json
    └── appsettings.json
```

## API Reference 📚

This project exposes a RESTful API for managing films and categories.

### Films Controller (`/api/Film`)

-   `GET /api/Film/Get`
    *   **Description:** Retrieves a list of all films.
    *   **Response:** `200 OK` with a JSON array of film objects.

-   `GET /api/Film/Get/{id}`
    *   **Description:** Retrieves a specific film by its ID.
    *   **Parameters:** `id` (int) - The ID of the film.
    *   **Response:** `200 OK` with the film object, `404 Not Found` if the film does not exist.

-   `POST /api/Film/Post`
    *   **Description:** Creates a new film.
    *   **Request Body:** JSON object representing the film to create.
    *   **Response:** `201 Created` on success.

-   `PUT /api/Film/Put`
    *   **Description:** Updates an existing film.
    *   **Request Body:** JSON object representing the film with updated information.
    *   **Response:** `204 No Content` on success, `404 Not Found` if the film does not exist.

-   `DELETE /api/Film/Delete?id={id}`
    *   **Description:** Deletes a film by its ID.
    *   **Parameters:** `id` (int) - The ID of the film to delete.
    *   **Response:** `204 No Content` on success, `404 Not Found` if the film does not exist.

-   `GET /api/Film/GetFilmCategory`
    *   **Description:** Retrieves a list of available film categories.
    *   **Response:** `200 OK` with a JSON array of category objects.

-   `GET /api/Film/Filter`
    *   **Description:** Filters films based on specified criteria.
    *   **Query Parameters:**
        *   `sortOrder` (string, optional): The order to sort the results by.
        *   `directorName` (string, optional): Filter by director's name.
        *   `categoryFilter` (string, optional): Filter by category.
    *   **Response:** `200 OK` with a JSON array of filtered film objects.

### Categories Controller (`/api/Category`)

-   `GET /api/Category/Get`
    *   **Description:** Retrieves a list of all categories.
    *   **Response:** `200 OK` with a JSON array of category objects.

-   `GET /api/Category/Get/{id}`
    *   **Description:** Retrieves a specific category by its ID.
    *   **Parameters:** `id` (int) - The ID of the category.
    *   **Response:** `200 OK` with the category object, `404 Not Found` if the category does not exist.

-   `POST /api/Category/Post`
    *   **Description:** Creates a new category.
    *   **Request Body:** JSON object representing the category to create.
    *   **Response:** `201 Created` on success.

-   `PUT /api/Category/Put`
    *   **Description:** Updates an existing category.
    *   **Request Body:** JSON object representing the category with updated information.
    *   **Response:** `204 No Content` on success, `404 Not Found` if the category does not exist.

-   `DELETE /api/Category/Delete?id={id}`
    *   **Description:** Deletes a category by its ID.
    *   **Parameters:** `id` (int) - The ID of the category to delete.
    *   **Response:** `204 No Content` on success, `404 Not Found` if the category does not exist.

## Contributing 🤝

Contributions are welcome! Please follow these steps:

1.  Fork the repository.
2.  Create a new branch (`git checkout -b feature/YourFeatureName`).
3.  Make your changes.
4.  Commit your changes (`git commit -am 'Add some feature'`).
5.  Push to the branch (`git push origin feature/YourFeatureName`).
6.  Create a new Pull Request.

Please ensure your code adheres to the project's coding standards and includes relevant tests if applicable.

## License 📜

This project is not specified with a license. Please refer to the repository for any licensing information.

## Important Links 🔗

-   **Repository:** [CatalogFilms](https://github.com/VortexLite/CatalogFilms)

## Footer 📝

© 2024 VortexLite. All rights reserved.

This project is licensed under the **MIT License** (if applicable, otherwise state 'No License').

[Back to Top](#catalogfilms) | [Report issue](https://github.com/VortexLite/CatalogFilms/issues) | [Request feature](https://github.com/VortexLite/CatalogFilms/issues)

--- 🚀

Star this repository ⭐ | Fork it 🍴 | Follow me on GitHub 🌟


---
**<p align="center">Generated by [ReadmeCodeGen](https://www.readmecodegen.com/)</p>**