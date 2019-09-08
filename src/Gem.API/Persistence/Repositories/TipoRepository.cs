using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gem.API.Domain.Models;
using Gem.API.Domain.Repositories;
using Gem.API.Persistence.Contexts;

namespace Gem.API.Persistence.Repositories
{
    public class TipoRepository : BaseRepository, ITipoRepository
    {
        public TipoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Tipo>> ListAsync()
        {
            return await _context.Tipos
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Tipo tipo)
        {
            await _context.Tipos.AddAsync(tipo);
        }

        public async Task<Tipo> FindByIdAsync(int id)
        {
            return await _context.Tipos.FindAsync(id);
        }

        public void Update(Tipo tipo)
        {
            _context.Tipos.Update(tipo);
        }

        public void Remove(Tipo tipo)
        {
            _context.Tipos.Remove(tipo);
        }
    }
}