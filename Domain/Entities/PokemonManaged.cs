using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;

namespace Domain.Entities
{
    public class PokemonManaged
    {
        public int Id { get; set; }
        public int ExternalId { get; set; } // Id da PokéAPI
        public string Name { get; set; } = string.Empty;
        public string TypesCsv { get; set; } = string.Empty; // Tipo do Pokemon
        public int BaseHp { get; set; }
        public HealthStatus Health { get; set; }
    }
}