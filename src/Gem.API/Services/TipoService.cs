using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Gem.API.Domain.Models;
using Gem.API.Domain.Repositories;
using Gem.API.Domain.Services;
using Gem.API.Domain.Services.Communication;
using Gem.API.Infrastructure;

namespace Gem.API.Services
{
    public class TipoService : ITipoService
    {
        private readonly ITipoRepository _tipoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public TipoService(ITipoRepository tipoRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _tipoRepository = tipoRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Tipo>> ListAsync()
        {
            // Aqui pegamos a lista de tipos do cache me memória. Se não houverem dados no cache, o método anonymous vai ser
            // chamado, setanto o cache para expirar um minuto depois e retornando a tarefa que lista os tipos do reposítório.
            var tipos = await _cache.GetOrCreateAsync(CacheKeys.TiposList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _tipoRepository.ListAsync();
            });
            
            return tipos;
        }

        public async Task<TipoResponse> SaveAsync(Tipo tipo)
        {
            try
            {
                await _tipoRepository.AddAsync(tipo);
                await _unitOfWork.CompleteAsync();

                return new TipoResponse(tipo);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TipoResponse($"Um erro ocorreu ao gravar o tipo: {ex.Message}");
            }
        }

        public async Task<TipoResponse> UpdateAsync(int id, Tipo tipo)
        {
            var existingTipo = await _tipoRepository.FindByIdAsync(id);

            if (existingTipo == null)
                return new TipoResponse("Tipo não encontrado.");

            existingTipo.Nome = tipo.Nome;

            try
            {
                _tipoRepository.Update(existingTipo);
                await _unitOfWork.CompleteAsync();

                return new TipoResponse(existingTipo);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TipoResponse($"Um erro ocorreu ao atualizar o tipo: {ex.Message}");
            }
        }

        public async Task<TipoResponse> DeleteAsync(int id)
        {
            var existingTipo = await _tipoRepository.FindByIdAsync(id);

            if (existingTipo == null)
                return new TipoResponse("Tipo não encontrado.");

            try
            {
                _tipoRepository.Remove(existingTipo);
                await _unitOfWork.CompleteAsync();

                return new TipoResponse(existingTipo);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new TipoResponse($"Um erro ocorreu ao deletar o tipo: {ex.Message}");
            }
        }
    }
}