using System.Threading.Tasks;

namespace Gem.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}