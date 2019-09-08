using System.Collections.Generic;
using System.Threading.Tasks;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;

namespace Gem.API.Domain.Repositories
{
    public interface IEntidadeRepository
    {
        Task<QueryResult<Entidade>> ListAsync(EntidadeQuery query);
        Task AddAsync(Entidade entidade);
        Task<Entidade> FindByIdAsync(int id);
        void Update(Entidade entidade);
        void Remove(Entidade entidade);
    }
}