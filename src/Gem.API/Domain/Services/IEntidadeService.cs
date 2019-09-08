using System.Threading.Tasks;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Domain.Services.Communication;

namespace Gem.API.Domain.Services
{
    public interface IEntidadeService
    {
        Task<QueryResult<Entidade>> ListAsync(EntidadeQuery query);
        Task<EntidadeResponse> SaveAsync(Entidade entidade);
        Task<EntidadeResponse> UpdateAsync(int id, Entidade entidade);
        Task<EntidadeResponse> DeleteAsync(int id);
    }
}