using Microsoft.AspNetCore.Mvc;
using SuperHeroDb.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroDb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        static readonly List<Comic> comics = new()
        {
            new Comic() { Id = 1, Name = "Marvel" },
            new Comic() { Id = 2, Name = "DC" }
        };

        static List<SuperHero> heroes = new()
        {
            new SuperHero() { Id = 1, FirstName = "Peter", LastName = "Parker", HeroName = "Spiderman", Comic = comics[0] },
            new SuperHero() { Id = 2, FirstName = "Bruce", LastName = "Wayne", HeroName = "Batman", Comic = comics[1] }
        };

        [HttpGet("comics")]
        public async Task<IActionResult> GeComics()
        {
            return Ok(comics);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuperHeroes()
        {
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleSuperHeroes(int id)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);

            if (hero == null)
                return NotFound("Super hero wasn't found. Too bad. :(");

            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuperHero(SuperHero hero)
        {
            if (heroes.Count == 0)
            {
                hero.Id = 1;
            }
            else
            {
                hero.Id = heroes.Max(i => i.Id + 1);
            }

            heroes.Add(hero);

            return Ok(heroes);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSuperHero(SuperHero superHero)
        {
            var dbHero = heroes.FirstOrDefault(h => h.Id == superHero.Id);

            if (dbHero is null)
                return NotFound("Super hero wasn't found. Too bad. :(");

            var index = heroes.IndexOf(dbHero);
            heroes[index] = superHero;

            return Ok(heroes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuperHero(int id)
        {
            var dbHero = heroes.FirstOrDefault(h => h.Id == id);

            if (dbHero is null)
                return NotFound("Super Hero wasn't found. Too bad. :(");

            heroes.Remove(dbHero);

            return Ok(heroes);
        }
    }
}