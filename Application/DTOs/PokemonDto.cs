using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    /// <summary>
    /// DTO para transferência de dados de Pokémon.
    /// </summary>
    public class PokemonDto
    {
        /// <summary>Identificador externo (API).</summary>
        public int ExternalId { get; set; }
        /// <summary>Nome do Pokémon.</summary>
        [Required]
        public required string Name { get; set; }
        /// <summary>Tipos do Pokémon (CSV).</summary>
        [Required]
        public required string TypesCsv { get; set; }
        /// <summary>Pontos de vida base.</summary>
        [Range(1, int.MaxValue)]
        public int BaseHp { get; set; }
    }
}
