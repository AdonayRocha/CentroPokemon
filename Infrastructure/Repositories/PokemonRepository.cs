using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;
public class PokemonRepository : IPokemonRepository
{
    private readonly PokemonDbContext _ctx;
    public PokemonRepository(PokemonDbContext ctx) => _ctx = ctx;


    public async Task AddAsync(PokemonManaged entity)
    {
        _ctx.Pokemons.Add(entity);
        await _ctx.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var e = await _ctx.Pokemons.FindAsync(id);
        if (e != null) { _ctx.Remove(e); await _ctx.SaveChangesAsync(); }
    }


    public Task<PokemonManaged?> GetByIdAsync(int id)
    => _ctx.Pokemons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);


    public Task<PokemonManaged?> GetByNameAsync(string name)
    => _ctx.Pokemons.FirstOrDefaultAsync(x => x.Name == name);


    public async Task<IReadOnlyList<PokemonManaged>> ListAsync()
    => await _ctx.Pokemons.AsNoTracking().OrderBy(x => x.Name).ToListAsync();


    public async Task UpdateAsync(PokemonManaged entity)
    {
        _ctx.Pokemons.Update(entity);
        await _ctx.SaveChangesAsync();
    }
}