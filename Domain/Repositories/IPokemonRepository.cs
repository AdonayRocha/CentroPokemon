using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<PokemonManaged>> GetAllAsync();
        Task<PokemonManaged?> GetByIdAsync(int id);
        Task AddAsync(PokemonManaged pokemon);
        Task UpdateAsync(PokemonManaged pokemon);
        Task DeleteAsync(PokemonManaged pokemon); 
    }

}
