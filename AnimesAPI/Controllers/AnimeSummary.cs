using Microsoft.AspNetCore.Mvc;

namespace AnimesAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeSummaryController : ControllerBase {

        private static List<Anime> animes = new List<Anime> {
            new Anime {
                Id = 1,
                Title = "Naruto",
                Genre = "Shounen",
                Score = 8,
                Status = Enums.StatusEnum.Completed
            },
            new Anime {
                Id = 2,
                Title = "One piece",
                Genre = "Shounen",
                Score = 9.5,
                Status = Enums.StatusEnum.Watching
            }
        };

        [HttpGet]
        public ActionResult<List<AnimeDTO>> GetAnimes() {
            var animesList = new List<AnimeDTO>();

            foreach (var anime in animes) {
                var animeDTO = new AnimeDTO() {
                    Title = anime.Title,
                    Genre = anime.Genre,
                    Score = anime.Score,
                };

                animesList.Add(animeDTO);
            }

            return animesList;
        }
    }
}
