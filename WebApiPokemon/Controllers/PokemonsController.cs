using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace WebApiPokemon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonsController : ControllerBase
    {
        private readonly PokemonService _pokemonService;

        public PokemonsController(PokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// Retorna um pokémon pelo ID.
        /// </summary>
        /// <param name="id">ID do pokémon.</param>
        /// <returns>Pokémon encontrado ou 404.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        /// <summary>
        /// Cria um novo pokémon.
        /// </summary>
        /// <param name="dto">Dados do pokémon.</param>
        /// <returns>Pokémon criado.</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _pokemonService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Atualiza um pokémon existente.
        /// </summary>
        /// <param name="id">ID do pokémon.</param>
        /// <param name="dto">Dados atualizados.</param>
        /// <returns>NoContent ou NotFound.</returns>
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _pokemonService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Remove um pokémon pelo ID.
        /// </summary>
        /// <param name="id">ID do pokémon.</param>
        /// <returns>NoContent ou NotFound.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pokemonService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Busca dados de um pokémon na API externa.
        /// </summary>
        /// <param name="name">Nome do pokémon.</param>
        /// <returns>Dados externos ou 404.</returns>
        [HttpGet("external/{name}")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFromExternalApi(string name)
        {
            var externalData = await _pokemonService.GetFromExternalApiAsync(name);
            if (externalData == null)
                return NotFound($"Pokemon '{name}' not found on external API.");

            return Ok(externalData);
        }

        /// <summary>
        /// Retorna um pokémon pelo nome.
        /// </summary>
        /// <param name="name">Nome do pokémon.</param>
        /// <returns>Pokémon encontrado ou 404.</returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var pokemons = await _pokemonService.GetAllAsync();
            var pokemon = pokemons.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }
    }
}
