using Microsoft.EntityFrameworkCore;

namespace AnimesAPI.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }
    }
}
