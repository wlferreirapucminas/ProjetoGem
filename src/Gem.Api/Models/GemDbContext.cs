using Microsoft.EntityFrameworkCore;

namespace GemApi.Models
{
    public class GemDbContext : DbContext
    {
        public GemDbContext(DbContextOptions<GemDbContext> options)
            : base(options)
        {
        }

        public DbSet<IgrejaItem> IgrejaItens { get; set; }
        public DbSet<EscolaItem> EscolaItens { get; set; }
    }
    
}