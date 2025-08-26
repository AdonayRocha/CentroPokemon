/// <summary>
/// DTO para transferência de dados de Pokémon.
/// </summary>
public class PokemonDto
{
    /// <summary>Identificador externo (API).</summary>
    public int ExternalId { get; set; }
    /// <summary>Nome do Pokémon.</summary>
    public string Name { get; set; }
    /// <summary>Tipos do Pokémon (CSV).</summary>
    public string TypesCsv { get; set; }
    /// <summary>Pontos de vida base.</summary>
    public int BaseHp { get; set; }
}