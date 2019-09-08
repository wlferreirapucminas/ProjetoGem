using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Gem.API.Domain.Models;
using Gem.API.Domain.Models.Queries;
using Gem.API.Domain.Repositories;
using Gem.API.Domain.Services;
using Gem.API.Domain.Services.Communication;
using Gem.API.Infrastructure;

namespace Gem.API.Services
{
    public class EntidadeService : IEntidadeService
    {
        private readonly IEntidadeRepository _entidadeRepository;
        private readonly ITipoRepository _tipoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public EntidadeService(IEntidadeRepository entidadeRepository, ITipoRepository tipoRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _entidadeRepository = entidadeRepository;
            _tipoRepository = tipoRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<QueryResult<Entidade>> ListAsync(EntidadeQuery query)
        {
            // Aqui é listado resultado da query se existir, mas os dados podem varia de acdordo com a ID do tipo ID, página e quantidade de
            // itens por página. O cache é composto para evitar o retorno errado de informação.
            string cacheKey = GetCacheKeyForEntidadeQuery(query);
            
            var entidade = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _entidadeRepository.ListAsync(query);
            });

            return entidade;
        }

        public async Task<EntidadeResponse> SaveAsync(Entidade entidade)
        {
            try
            {
                /*
                 Temos que verificar se a ID do tipo ID é valida antes de adicionar a entidade, para evitar erros.
                 Podemos criar um método na classe TipoService para retornar a categoria e inserir o serviço aqui.
                */
                var existingTipo = await _tipoRepository.FindByIdAsync(entidade.TipoId);
                if (existingTipo == null)
                    return new EntidadeResponse("Categoria inválida.");

                await _entidadeRepository.AddAsync(entidade);
                await _unitOfWork.CompleteAsync();

                return new EntidadeResponse(entidade);
            }
            catch (Exception ex)
            {
                // Logging
                return new EntidadeResponse($"Um erro ocorreu ao salvar a entidade: {ex.Message}");
            }
        }

        public async Task<EntidadeResponse> UpdateAsync(int id, Entidade entidade)
        {
            var existingEntidade = await _entidadeRepository.FindByIdAsync(id);

            if (existingEntidade == null)
                return new EntidadeResponse("Entidade not found.");

            var existingTipo = await _tipoRepository.FindByIdAsync(entidade.TipoId);
            if (existingTipo == null)
                return new EntidadeResponse("Tipo inválido.");

            existingEntidade.Nome = entidade.Nome;
            existingEntidade.Endereco = entidade.Endereco;
            existingEntidade.Cargo = entidade.Cargo;
            existingEntidade.Status = entidade.Status;
            existingEntidade.Email = entidade.Email;
            existingEntidade.TipoId = entidade.TipoId;

            try
            {
                _entidadeRepository.Update(existingEntidade);
                await _unitOfWork.CompleteAsync();

                return new EntidadeResponse(existingEntidade);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EntidadeResponse($"Um erro ocorreu ao atualizar a entidade: {ex.Message}");
            }
        }

        public async Task<EntidadeResponse> DeleteAsync(int id)
        {
            var existingEntidade = await _entidadeRepository.FindByIdAsync(id);

            if (existingEntidade == null)
                return new EntidadeResponse("Entidade não encontrada.");

            try
            {
                _entidadeRepository.Remove(existingEntidade);
                await _unitOfWork.CompleteAsync();

                return new EntidadeResponse(existingEntidade);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EntidadeResponse($"Um erro ocorreu ao deletar a entidade: {ex.Message}");
            }
        }

        private string GetCacheKeyForEntidadeQuery(EntidadeQuery query)
        {
            string key = CacheKeys.EntidadeList.ToString();
            
            if (query.TipoId.HasValue && query.TipoId > 0)
            {
                key = string.Concat(key, "_", query.TipoId.Value);
            }

            key = string.Concat(key, "_", query.Page, "_", query.ItemsPerPage);
            return key;
        }
    }
}