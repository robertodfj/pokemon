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

        [HttpPost("capture")]
        [Authorize]
        public async Task<IActionResult> CapturePokemon([FromBody] CreatePokemonDTO createPokemonDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Invalid user ID.");
            }

            _logger.LogInformation("Capturing new Pokemon: {Name} by user {UserId}", createPokemonDTO.Name, userId.Value);

            var result = await _pokemonService.CreatePokemon(createPokemonDTO, int.Parse(userId.Value));

            return Ok(result);
        }
    }
}