using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

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

        public async Task<PokemonManaged> CreateOrUpdateFromExternalAsync(string name, CancellationToken ct = default)
        {
            var ext = await _pokeApi.GetPokemonAsync(name, ct) ?? throw new InvalidOperationException("Pokémon não encontrado");
            var baseHp = ext.stats?.FirstOrDefault(s => s.stat?.name == "hp")?.base_stat ?? 0;
            var types = ext.types != null
                ? string.Join(',', ext.types.OrderBy(t => t.slot).Select(t => t.type?.name ?? string.Empty))
                : string.Empty;
            var health = ClassifyHealth(baseHp);

            var existing = await _repo.GetByNameAsync(ext.name);
            if (existing is null)
            {
                var entity = new PokemonManaged
                {
                    ExternalId = ext.id,
                    Name = ext.name,
                    TypesCsv = types,
                    BaseHp = baseHp,
                    Health = health
                };
                await _repo.AddAsync(entity);
                return entity;
            }
            else
            {
                existing.ExternalId = ext.id;
                existing.TypesCsv = types;
                existing.BaseHp = baseHp;
                existing.Health = health;
                await _repo.UpdateAsync(existing);
                return existing;
            }
        }

        public static HealthStatus ClassifyHealth(int baseHp)
            => baseHp <= 40 ? HealthStatus.Ruim
            : baseHp <= 60 ? HealthStatus.Media
            : HealthStatus.Saudavel;
    }
}