using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class theMovieDb
{
    public event EventHandler MoviesListChanged;

    public Movies MoviesList { get; set; }
    private readonly HttpClient httpClient;
    private const string apiKey = "8f781d70654b5a6f2fa69770d1d115a3";
    public class Result
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Movies
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public theMovieDb(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task SearchMoviesAsync(string query)
    {
        try
        {
            string apiUrl = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={query}";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MoviesList = JsonSerializer.Deserialize<Movies>(content);
                MoviesListChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Handle the error
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
        }
    }
}
