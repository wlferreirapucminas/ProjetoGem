using Gem.API.Domain.Models;

namespace Gem.API.Domain.Services.Communication
{
    public class TipoResponse : BaseResponse<Tipo>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="tipo">Saved tipo.</param>
        /// <returns>Response.</returns>
        public TipoResponse(Tipo tipo) : base(tipo)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public TipoResponse(string message) : base(message)
        { }
    }
}