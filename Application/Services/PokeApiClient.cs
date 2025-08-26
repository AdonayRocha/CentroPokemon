using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;

public class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;

    public PokeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokeApiPokemon?> GetPokemonAsync(string nameOrId, CancellationToken ct = default)
    {
        var url = $"https://pokeapi.co/api/v2/pokemon/{nameOrId.ToLower()}";
        var response = await _httpClient.GetAsync(url, ct);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync(ct);
        var pokemon = JsonSerializer.Deserialize<PokeApiPokemon>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return pokemon;
    }

    public async Task<PokemonDto?> GetPokemonByNameAsync(string name)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        var pokemon = JsonSerializer.Deserialize<PokemonDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return pokemon;
    }
}