using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Domain.Repositories;
using Gem.API.Persistence.Contexts;

namespace Gem.API.Persistence.Repositories
{
    public class EntidadeRepository : BaseRepository, IEntidadeRepository
    {
        public EntidadeRepository(AppDbContext context) : base(context) { }

        public async Task<QueryResult<Entidade>> ListAsync(EntidadeQuery query)
        {
            IQueryable<Entidade> queryable = _context.Entidade
                                                    .Include(p => p.Tipo)
                                                    .AsNoTracking(); 
                                    
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if(query.TipoId.HasValue && query.TipoId > 0)
            {
                queryable = queryable.Where(p => p.TipoId == query.TipoId);
            }
            
            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();
            
            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            List<Entidade> entidade = await queryable.Skip((query.Page - query.ItemsPerPage) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client calculations of pages).
            return new QueryResult<Entidade>
            {
                Items = entidade,
                TotalItems = totalItems,
            };
        }

        public async Task<Entidade> FindByIdAsync(int id)
        {
            return await _context.Entidade
                                 .Include(p => p.Tipo)
                                 .FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method return, we can't use FindAsync
        }

        public async Task AddAsync(Entidade entidade)
        {
            await _context.Entidade.AddAsync(entidade);
        }

        public void Update(Entidade entidade)
        {
            _context.Entidade.Update(entidade);
        }

        public void Remove(Entidade entidade)
        {
            _context.Entidade.Remove(entidade);
        }
    }
}