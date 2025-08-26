using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.DTOs;
using Application.Contracts; 

namespace WebApiPokemon.Controllers
{
    public class PokeApiClient : IPokeApiClient 
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokeApiClient> _logger;

        public PokeApiClient(HttpClient httpClient, ILogger<PokeApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            if (_httpClient.BaseAddress == null)
                _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<PokeApiPokemon?> GetPokemonAsync(string nameOrId, CancellationToken ct = default)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon/{nameOrId.ToLower()}", ct);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("PokéAPI returned {Status} for {NameOrId}", response.StatusCode, nameOrId);
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync(ct);

                var pokemon = JsonSerializer.Deserialize<PokeApiPokemon>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return pokemon;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling PokéAPI for {NameOrId}", nameOrId);
                return null;
            }
        }
    }
}
