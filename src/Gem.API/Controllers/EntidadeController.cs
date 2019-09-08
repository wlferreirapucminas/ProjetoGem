using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Domain.Services;
using Gem.API.Resources;

namespace Gem.API.Controllers
{
    [Route("/api/entidade")]
    [Produces("application/json")]
    [ApiController]
    public class EntidadeController : Controller
    {
        private readonly IEntidadeService _entidadeService;
        private readonly IMapper _mapper;

        public EntidadeController(IEntidadeService entidadeService, IMapper mapper)
        {
            _entidadeService = entidadeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as entidades.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<EntidadeResource>), 200)]
        public async Task<QueryResultResource<EntidadeResource>> ListAsync([FromQuery] EntidadeQueryResource query)
        {
            var entidadeQuery = _mapper.Map<EntidadeQueryResource, EntidadeQuery>(query);
            var queryResult = await _entidadeService.ListAsync(entidadeQuery);

            var resource = _mapper.Map<QueryResult<Entidade>, QueryResultResource<EntidadeResource>>(queryResult);
            return resource;
        }

        /// <summary>
        /// Salva uma nova entidade.
        /// </summary>
        /// <param name="resource">Entidade data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EntidadeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveEntidadeResource resource)
        {
            var entidade = _mapper.Map<SaveEntidadeResource, Entidade>(resource);
            var result = await _entidadeService.SaveAsync(entidade);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var entidadeResource = _mapper.Map<Entidade, EntidadeResource>(result.Resource);
            return Ok(entidadeResource);
        }

        /// <summary>
        /// Atualiza uma entidade de acordo com o id.
        /// </summary>
        /// <param name="id">ID da Entidade.</param>
        /// <param name="resource">Dados da Entidade.</param>
        /// <returns>Resposta de solicitacao.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EntidadeResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEntidadeResource resource)
        {
            var entidade = _mapper.Map<SaveEntidadeResource, Entidade>(resource);
            var result = await _entidadeService.UpdateAsync(id, entidade);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var entidadeResource = _mapper.Map<Entidade, EntidadeResource>(result.Resource);
            return Ok(entidadeResource);
        }

        /// <summary>
        /// Deleta uma Entidade de acordo com a id.
        /// </summary>
        /// <param nome="id">Entidade identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EntidadeResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _entidadeService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var tipoResource = _mapper.Map<Entidade, EntidadeResource>(result.Resource);
            return Ok(tipoResource);
        }
    }
}