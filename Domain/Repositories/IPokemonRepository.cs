using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
namespace Domain.Repositories;
public interface IPokemonRepository
{
    Task<PokemonManaged?> GetByIdAsync(int id);
    Task<PokemonManaged?> GetByNameAsync(string name);
    Task AddAsync(PokemonManaged entity);
    Task UpdateAsync(PokemonManaged entity);
    Task DeleteAsync(int id);
    Task<IReadOnlyList<PokemonManaged>> ListAsync();
}