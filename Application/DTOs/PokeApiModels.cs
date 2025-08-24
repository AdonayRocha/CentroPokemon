using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;
public sealed class PokeApiPokemon
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public List<PokeTypeSlot> types { get; set; } = new();
    public List<PokeStat> stats { get; set; } = new();
}
public sealed class PokeTypeSlot { public int slot { get; set; } public PokeType type { get; set; } = new(); }
public sealed class PokeType { public string name { get; set; } = string.Empty; }
public sealed class PokeStat { public int base_stat { get; set; } public PokeStatName stat { get; set; } = new(); }
public sealed class PokeStatName { public string name { get; set; } = string.Empty; }