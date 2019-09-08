using System.Collections.Generic;
using System.Threading.Tasks;
using Gem.API.Domain.Models;

namespace Gem.API.Domain.Repositories
{
    public interface ITipoRepository
    {
        Task<IEnumerable<Tipo>> ListAsync();
        Task AddAsync(Tipo tipo);
        Task<Tipo> FindByIdAsync(int id);
        void Update(Tipo tipo);
        void Remove(Tipo tipo);
    }
}