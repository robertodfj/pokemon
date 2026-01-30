using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokemon.dto;
using Pokemon.service;

namespace Pokemon.controller
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(PokemonService pokemonService, ILogger<PokemonController> logger)
        {
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPokemons()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Invalid user ID.");
            }

            _logger.LogInformation("Fetching Pokemons for user {UserId}", userId.Value);

            var pokemons = await _pokemonService.GetPokemonsByUserId(int.Parse(userId.Value));

            return Ok(pokemons);
        }

        [HttpPost("capture")]
        [Authorize]
        public async Task<IActionResult> CapturePokemon([FromBody] CreatePokemonDTO createPokemonDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Invalid user ID.");
            }

            _logger.LogInformation("Capturing new Pokemon by user {UserId}", userId.Value);

            var result = await _pokemonService.CreatePokemon(createPokemonDTO, int.Parse(userId.Value));

            return Ok(result);
        }

        [HttpDelete("release/{PokemonID}")]
        [Authorize]
        public async Task<IActionResult> ReleasePokemon(int PokemonID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Invalid user ID.");
            }

            _logger.LogInformation("Releasing Pokemon: {PokemonId} by user {UserId}", PokemonID, userId.Value);

            var result = await _pokemonService.DeletePokemon(PokemonID, int.Parse(userId.Value));

            return Ok(result);
        }
    }
}