using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gem.API.Domain.Models;
using Gem.API.Domain.Services;
using Gem.API.Resources;

namespace Gem.API.Controllers
{
    [Route("/api/tipos")]
    [Produces("application/json")]
    [ApiController]
    public class TiposController : Controller
    {
        private readonly ITipoService _tipoService;
        private readonly IMapper _mapper;

        public TiposController(ITipoService tipoService, IMapper mapper)
        {
            _tipoService = tipoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos os tipos.
        /// </summary>
        /// <returns>Lista os tipos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TipoResource>), 200)]
        public async Task<IEnumerable<TipoResource>> ListAsync()
        {
            var tipos = await _tipoService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Tipo>, IEnumerable<TipoResource>>(tipos);

            return resources;
        }

        /// <summary>
        /// Salva um novo tipo.
        /// </summary>
        /// <param name="resource">Tipo de entidade.</param>
        /// <returns>Resposta para solicitação.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TipoResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTipoResource resource)
        {
            var tipo = _mapper.Map<SaveTipoResource, Tipo>(resource);
            var result = await _tipoService.SaveAsync(tipo);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var tipoResource = _mapper.Map<Tipo, TipoResource>(result.Resource);
            return Ok(tipoResource);
        }

        /// <summary>
        /// Atualiza um tipo existente de acordo com a id.
        /// </summary>
        /// <param name="id">Tipo da entidade.</param>
        /// <param name="resource">Atualiza o tipo da entidade.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TipoResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTipoResource resource)
        {
            var tipo = _mapper.Map<SaveTipoResource, Tipo>(resource);
            var result = await _tipoService.UpdateAsync(id, tipo);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var tipoResource = _mapper.Map<Tipo, TipoResource>(result.Resource);
            return Ok(tipoResource);
        }

        /// <summary>
        /// Deleta um tipo de acordo com a id.
        /// </summary>
        /// <param name="id">Tipo da entidade.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TipoResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _tipoService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var tipoResource = _mapper.Map<Tipo, TipoResource>(result.Resource);
            return Ok(tipoResource);
        }
    }
}