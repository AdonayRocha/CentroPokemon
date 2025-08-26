/// <summary>
/// DTO para transfer�ncia de dados de Pok�mon.
/// </summary>
public class PokemonDto
{
    /// <summary>Identificador externo (API).</summary>
    public int ExternalId { get; set; }
    /// <summary>Nome do Pok�mon.</summary>
    public string Name { get; set; }
    /// <summary>Tipos do Pok�mon (CSV).</summary>
    public string TypesCsv { get; set; }
    /// <summary>Pontos de vida base.</summary>
    public int BaseHp { get; set; }
}