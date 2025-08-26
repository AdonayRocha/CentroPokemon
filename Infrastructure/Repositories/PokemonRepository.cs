using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonDbContext _ctx;

        public PokemonRepository(PokemonDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<PokemonManaged>> GetAllAsync()
            => await _ctx.Pokemons.ToListAsync();

        public async Task<PokemonManaged?> GetByIdAsync(int id)
            => await _ctx.Pokemons.FindAsync(id);

        public async Task AddAsync(PokemonManaged pokemon)
        {
            _ctx.Pokemons.Add(pokemon);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(PokemonManaged pokemon)
        {
            _ctx.Pokemons.Update(pokemon);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(PokemonManaged pokemon)
        {
            _ctx.Pokemons.Remove(pokemon);
            await _ctx.SaveChangesAsync();
        }
    }
}
