using SuperHeroDb.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHeroDb.Client.Services
{
    public interface ISuperHeroService
    {
        event Action OnChange;
        List<Comic> Comics { get; set; }
        List<SuperHero> Heroes { get; set; }
        Task<List<SuperHero>> GetSuperHeroes();
        Task GetComics();
        Task<SuperHero> GetSuperHero(int id);
        Task<List<SuperHero>> CreateSuperHero(SuperHero superHero);
        Task<List<SuperHero>> UpdateSuperHero(SuperHero superHero);
        Task<List<SuperHero>> DeleteSuperHero(int id);      
    }
}