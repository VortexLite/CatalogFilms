class FilmService {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getFilm() {
        const response = await fetch(`${this.baseUrl}/Get`);
        return await response.json();
    }

    async getFilm(id) {
        const response = await fetch(`${this.baseUrl}/Get/${id}`);
        return await response.json();
    }

    async addFilm(model) {
        const response = await fetch(`${this.baseUrl}/Post`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        });
        return await response.text();
    }

    async updateFilm(model) {
        const response = await fetch(`${this.baseUrl}/Put`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(model)
        });
        return await response.text();
    }

    async deleteFilm(id) {
        const response = await fetch(`${this.baseUrl}/Delete?id=${id}`, {
            method: 'DELETE'
        });
        return await response.text();
    }

    async GetFilmCategory() {
        const response = await fetch(`${this.baseUrl}/GetFilmCategory`);
        return await response.json();
    }

    async Filter(sortOrder, directorName, categoryFilter) {
        const response = await fetch(`${this.baseUrl}/Filter?sortOrder=${sortOrder}&directorName=${directorName}&categoryFilter=${categoryFilter}`);
        return await response.json();
    }
}