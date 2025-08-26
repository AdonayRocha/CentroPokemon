using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;
using Domain.Entities; 

namespace Domain.Entities
{
    /// <summary>
    /// Representa um Pokémon gerenciado no sistema.
    /// </summary>
    public class PokemonManaged
    {
        /// <summary>Identificador interno.</summary>
        public int Id { get; set; }
        /// <summary>Identificador externo (API).</summary>
        public int ExternalId { get; set; }
        /// <summary>Nome do Pokémon.</summary>
        public string Name { get; set; }
        /// <summary>Tipos do Pokémon (CSV).</summary>
        public string TypesCsv { get; set; }
        /// <summary>Pontos de vida base.</summary>
        public int BaseHp { get; set; }
        /// <summary>Status de saúde.</summary>
        public HealthStatus Health { get; set; }
    }
}