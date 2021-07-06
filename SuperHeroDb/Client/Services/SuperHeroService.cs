using SuperHeroDb.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SuperHeroDb.Client.Services
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly HttpClient _httpClient;

        public SuperHeroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<Comic> Comics { get; set; } = new();
        public List<SuperHero> Heroes { get; set; } = new();

        public event Action OnChange;

        public async Task<List<SuperHero>> CreateSuperHero(SuperHero superHero)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/superhero", superHero);

            Heroes = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            OnChange.Invoke();

            return Heroes;
        }

        public async Task<List<SuperHero>> DeleteSuperHero(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/superhero/{id}");
            
            Heroes = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            OnChange.Invoke();

            return Heroes;
        }

        public async Task GetComics()
        {
            Comics = await _httpClient.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
        }

        public async Task<SuperHero> GetSuperHero(int id)
        {
            return await _httpClient.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
        }

        public async Task<List<SuperHero>> GetSuperHeroes()
        {
            Heroes = await _httpClient.GetFromJsonAsync<List<SuperHero>>("api/superhero");

            return Heroes;
        }

        public async Task<List<SuperHero>> UpdateSuperHero(SuperHero superHero)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/superhero", superHero);

            Heroes = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            OnChange.Invoke();

            return Heroes;
        }
    }
}