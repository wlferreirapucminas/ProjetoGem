using Gem.API.Domain.Models;

namespace Gem.API.Domain.Services.Communication
{
    public class EntidadeResponse : BaseResponse<Entidade>
    {
        public EntidadeResponse(Entidade entidade) : base(entidade) { }

        public EntidadeResponse(string message) : base(message) { }
    }
}