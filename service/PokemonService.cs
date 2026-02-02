using System.Text.Json;
using Pokemon.data;
using Pokemon.dto;
using Microsoft.EntityFrameworkCore;
using Pokemon.middleware.custom_exception;

namespace Pokemon.service
{
    public class PokemonService
    {
        private readonly AppDBContext _context;
        private readonly HttpClient _httpClient;

        public PokemonService(AppDBContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<PokemonResponseDTO> CreatePokemon(CreatePokemonDTO createPokemonDTO, int userId)
        {
            if (await HasUserPokemon(createPokemonDTO.PokemonApiId, userId))
            {
                throw new ConflictException("User already has this pokemon.");
            }
            if (CaptureSuccess(createPokemonDTO.Level) == false)
            {
                throw new BadRequestException("Pokemon capture failed.");
            }
            // Obtener el pokemomn de la API externa
            // Primero obtengo los daos princiopales del pokemon
            var apiResponse = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{createPokemonDTO.PokemonApiId}");
            apiResponse.EnsureSuccessStatusCode();
            var jsonResponse = await apiResponse.Content.ReadAsStringAsync();
            var pokemonApi = JsonSerializer.Deserialize<PokemonApiDTO>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            // Segundo obtengo la categoria / especie del pokemon
            var apiResponseSpecies = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{createPokemonDTO.PokemonApiId}");
            apiResponseSpecies.EnsureSuccessStatusCode();
            var jsonResponseSpecies = await apiResponseSpecies.Content.ReadAsStringAsync();
            var pokemonSpeciesApi = JsonSerializer.Deserialize<PokemonSpeciesDTO>(jsonResponseSpecies, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            // Guardar el pokemon en la db
            if (pokemonApi == null || pokemonApi.Sprites == null)
                throw new BadRequestException("Failed to fetch Pokemon data from API.");
            if (pokemonSpeciesApi == null)
                throw new BadRequestException("Failed to fetch Pokemon species data.");
            if (pokemonSpeciesApi == null)
                throw new BadRequestException("Failed to fetch Pokemon species data.");

            var category = pokemonSpeciesApi.Genera
                  .FirstOrDefault(g => g.Language.Name == "en")?.Genus ?? "Unknown";    
    
            var newPokemon = new model.Pokemon()
            {
                Name = pokemonApi.Name,
                Category = category,
                ImageURL = createPokemonDTO.IsShiny ? pokemonApi.Sprites.Front_Shiny : pokemonApi.Sprites.Front_Default,
                IsShiny = createPokemonDTO.IsShiny,
                Level = createPokemonDTO.Level,
                OwnerId = userId
            };
            _context.Pokemons.Add(newPokemon);
            await _context.SaveChangesAsync();

            // Hacer el retorno de la respuesta con el DTO
            return new PokemonResponseDTO()
            {
                Id = newPokemon.Id,
                Name = newPokemon.Name,
                Category = newPokemon.Category,
                ImageURL = newPokemon.ImageURL,
                IsShiny = newPokemon.IsShiny,
                Level = newPokemon.Level,
                OwnerId = userId
            };
        }

        public async Task<List<PokemonResponseDTO>> GetPokemonsByUserId(int userId)
        {
            var pokemons = await _context.Pokemons.Where(p => p.OwnerId == userId).ToListAsync();

            return pokemons.Select(p => new PokemonResponseDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                ImageURL = p.ImageURL,
                IsShiny = p.IsShiny,
                Level = p.Level,
                OwnerId = p.OwnerId
            }).ToList();
        }

        public async Task<model.Pokemon> DeletePokemon(int pokemonId, int userId)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == pokemonId && p.OwnerId == userId);
            if (pokemon == null)
            {
                throw new NotFoundException("Pokemon not found for this user.");
            }

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task<bool> HasUserPokemon(int pokemonId, int userId)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == pokemonId && p.OwnerId == userId);
        }

        public bool CaptureSuccess(int PokemonLevel)
        {
            var captureChance = Math.Max(20, 100 - PokemonLevel); // A mayor nivel, menor probabilidad
            var roll = Random.Shared.Next(1, 101); // El % va de 1 a 100
            return roll <= captureChance;
        }
    }
}