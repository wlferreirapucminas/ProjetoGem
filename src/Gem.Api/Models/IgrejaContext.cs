using Microsoft.EntityFrameworkCore;

namespace GemApi.Models
{
    public class IgrejaContext : DbContext
    {
        public IgrejaContext(DbContextOptions<IgrejaContext> options)
            : base(options)
        {
        }

        public DbSet<IgrejaItem> IgrejaItens { get; set; }
    }
}