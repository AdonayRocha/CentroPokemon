using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Services;

namespace Application.Services
{
    public class PokemonService
    {
        private readonly IPokeApiClient _pokeApi;
        private readonly IPokemonRepository _repo;

        public PokemonService(IPokeApiClient pokeApi, IPokemonRepository repo)
        {
            _pokeApi = pokeApi;
            _repo = repo;
        }

        // CRUD

        public async Task<IEnumerable<PokemonManaged>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<PokemonManaged?> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<PokemonManaged> AddAsync(PokemonDto dto)
        {
            var entity = new PokemonManaged
            {
                Name = dto.Name,
                TypesCsv = dto.TypesCsv,
                BaseHp = dto.BaseHp,
                Health = ClassifyHealth(dto.BaseHp)
            };
            await _repo.AddAsync(entity);
            return entity;
        }

        public async Task<bool> UpdateAsync(int id, PokemonDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Name = dto.Name;
            entity.TypesCsv = dto.TypesCsv;
            entity.BaseHp = dto.BaseHp;
            entity.Health = ClassifyHealth(dto.BaseHp);

            await _repo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return false;

            await _repo.DeleteAsync(entity);
            return true;
        }

        // API externa
        public async Task<PokemonManaged?> GetFromExternalApiAsync(string name, CancellationToken ct = default)
        {
            var ext = await _pokeApi.GetPokemonAsync(name, ct);
            if (ext == null) return null;

            var baseHp = ext.stats?.FirstOrDefault(s => s.stat?.name == "hp")?.base_stat ?? 0;
            var types = ext.types != null
                ? string.Join(',', ext.types.OrderBy(t => t.slot).Select(t => t.type?.name ?? string.Empty))
                : string.Empty;

            return new PokemonManaged
            {
                ExternalId = ext.id,
                Name = ext.name,
                TypesCsv = types,
                BaseHp = baseHp,
                Health = ClassifyHealth(baseHp)
            };
        }

        public static HealthStatus ClassifyHealth(int baseHp)
            => baseHp <= 40 ? HealthStatus.Ruim
            : baseHp <= 60 ? HealthStatus.Media
            : HealthStatus.Saudavel;
    }
}
