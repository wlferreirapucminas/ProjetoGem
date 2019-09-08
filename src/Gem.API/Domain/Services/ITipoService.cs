using System.Collections.Generic;
using System.Threading.Tasks;
using Gem.API.Domain.Models;
using Gem.API.Domain.Services.Communication;

namespace Gem.API.Domain.Services
{
    public interface ITipoService
    {
         Task<IEnumerable<Tipo>> ListAsync();
         Task<TipoResponse> SaveAsync(Tipo tipo);
         Task<TipoResponse> UpdateAsync(int id, Tipo tipo);
         Task<TipoResponse> DeleteAsync(int id);
    }
}