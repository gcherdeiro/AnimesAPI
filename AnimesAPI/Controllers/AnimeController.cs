using AnimesAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AnimesAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase {

        private static List<Anime> animes = new List<Anime> {
            new Anime() {
                Id = 1,
                Title = "One Piece",
                Genre = "Adventure",
                Score = 10,
                Status = StatusEnum.Watching
            },
            new Anime() {
                Id = 2,
                Title = "Naruto",
                Genre = "Action",
                Score = 9.5,
                Status = StatusEnum.Completed
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<Anime>>> GetAnimeList() {
            if (animes == null)
                return BadRequest("Não existem animes na lista.");

            return Ok(animes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnimeById(int id) {
            if (animes == null )
                return BadRequest("Não existem animes na lista.");

            var anime = animes.Find(f => f.Id == id);

            return Ok(anime);
        }

        [HttpPost]
        public async Task<ActionResult<List<Anime>>> AddAnime(Anime anime) {
            if (animes == null)
                return BadRequest("Não existem animes na lista.");

            animes.Add(anime);
            
            return Ok(animes);
        }

        [HttpPut]
        public async Task<ActionResult<List<Anime>>> UpdateAnime(Anime request) {
            if (animes == null)
                return BadRequest("Não existem animes na lista.");

            var anime = animes.Find(f => f.Id == request.Id);

            anime.Title = request.Title;
            anime.Genre = request.Genre;
            anime.Score = request.Score;
            anime.Status = request.Status;

            return Ok(animes);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Anime>>> DeleteAnime(int id) {
            if (animes == null)
                return BadRequest("Não existem animes na lista.");

            var anime = animes.Find(f => f.Id == id);
            if (anime == null)
                return BadRequest("Id inválido.");

            animes.Remove(anime);

            return Ok(animes);
        }
    }
}
