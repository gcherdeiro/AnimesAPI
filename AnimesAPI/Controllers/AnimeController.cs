using AnimesAPI.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AnimesAPI.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimeController : ControllerBase {

        private readonly DataContext _context;

        public AnimeController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        [ActionName("GetAnimeList")]
        public async Task<ActionResult<List<Anime>>> GetAnimeList() {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list.");

            return Ok(await _context.Animes.ToListAsync());
        }

        [HttpGet("{id}")]
        [ActionName("GetAnimeById")]
        public async Task<ActionResult<Anime>> GetAnimeById(int id) {
            var anime = await _context.Animes.FindAsync(id);

            if (anime == null )
                return BadRequest("Anime not found.");

            return Ok(anime);
        }

        [HttpGet]
        [ActionName("GetAnimeByRank")]
        public async Task<ActionResult<List<Anime>>> GetAnimeByRank() {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list.");

            var rankedAnimes = await _context.Animes.Select(p => new { p.Title, p.Score })
                                                    .Where(p => p.Score != null)
                                                    .OrderByDescending(p => p.Score)
                                                    .ToListAsync();

            return Ok(rankedAnimes);
        }

        [HttpGet]
        [ActionName("GetAnimeByStatus")]
        public async Task<ActionResult<List<Anime>>> GetAnimeByStatus(int requestedStatus) {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list.");

            var animesByStatus = await _context.Animes.Select(p => new { p.Title, p.Genre, p.Status })
                                                      .Where(p => (int)p.Status == requestedStatus)
                                                      .ToListAsync();

            if (animesByStatus.Count == 0)
                return NotFound("There is no anime with the selected status.");
                
            return Ok(animesByStatus);
        }

        [HttpGet]
        [ActionName("GetAnimeByTitle")]
        public async Task<ActionResult<List<Anime>>> GetAnimeByTitle(string requestedTitle) {
            if (await _context.Animes.ToArrayAsync() == null)
                return BadRequest("There is no animes on this list.");

            var animeTitle = await _context.Animes.Where(p => p.Title.Contains(requestedTitle))
                                                  .ToListAsync();

            if (animeTitle.Count == 0)
                return NotFound("There is no anime on this list with the selected title.");

            return Ok(animeTitle);
        }

        [HttpGet]
        [ActionName("GetAnimeByGenre")]
        public async Task<ActionResult<List<Anime>>> GetAnimeByGenre(string requestedGenre) {
            if (await _context.Animes.ToArrayAsync() == null)
                return BadRequest("There is no animes on this list.");

            var animesByGenre = await _context.Animes.Where(p => p.Genre.Contains(requestedGenre))
                                                     .ToListAsync();

            if (animesByGenre.Count == 0)
                return NotFound("There is no anime on this list with the selected genre.");

            return Ok(animesByGenre);
        }

        [HttpPost]
        [ActionName("AddAnime")]
        public async Task<ActionResult<List<Anime>>> AddAnime(Anime anime) {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list.");

            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.Animes.ToListAsync());
        }

        [HttpPut]
        [ActionName("UpdateAnime")]
        public async Task<ActionResult<List<Anime>>> UpdateAnime(Anime anime) {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list..");

            var dbAnime = await _context.Animes.FindAsync(anime.Id);

            dbAnime.Title = anime.Title;
            dbAnime.Genre = anime.Genre;
            dbAnime.Score = anime.Score;
            dbAnime.Status = anime.Status;

            await _context.SaveChangesAsync();

            return Ok(await _context.Animes.ToListAsync());
        }

        [HttpDelete]
        [ActionName("DeleteAnime")]
        public async Task<ActionResult<List<Anime>>> DeleteAnime(int id) {
            if (await _context.Animes.ToListAsync() == null)
                return BadRequest("There is no animes on this list.");

            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
                return BadRequest("Invalid id.");

            _context.Animes.Remove(anime);

            await _context.SaveChangesAsync();

            return Ok(await _context.Animes.ToListAsync());
        }

        //[HttpGet]
        //public ActionResult<List<Anime>> GetAnimes() {
        //    var animeDTO = new AnimeDTO() {

        //    };

        //    return animes;
        //}
    }
}
