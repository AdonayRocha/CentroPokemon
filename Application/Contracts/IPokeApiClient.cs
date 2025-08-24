using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
namespace Application.Contracts;
public interface IPokeApiClient
{
    Task<PokeApiPokemon?> GetPokemonAsync(string nameOrId, CancellationToken ct = default);
}