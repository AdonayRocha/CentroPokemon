using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace WebApiBanco.Controllers
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

        // GET: api/pokemons
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pokemons = await _pokemonService.GetAllAsync();
            return Ok(pokemons);
        }

        // GET: api/pokemons/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pokemon = await _pokemonService.GetByIdAsync(id);
            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        // POST: api/pokemons
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _pokemonService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/pokemons/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PokemonDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _pokemonService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/pokemons/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _pokemonService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // GET: api/pokemons/external/{name}
        [HttpGet("external/{name}")]
        public async Task<IActionResult> GetFromExternalApi(string name)
        {
            var externalData = await _pokemonService.GetFromExternalApiAsync(name);
            if (externalData == null)
                return NotFound($"Pokemon '{name}' not found on external API.");

            return Ok(externalData);
        }
    }
}
