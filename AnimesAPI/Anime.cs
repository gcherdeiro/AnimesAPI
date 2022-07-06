using AnimesAPI.Enums;

namespace AnimesAPI {
    public class Anime {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double? Score { get; set; }
        public StatusEnum Status { get; set; }
    }
}
