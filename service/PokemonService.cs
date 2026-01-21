using System.Text.Json;
using Pokemon.data;
using Pokemon.dto;

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
            var newPokemon = new model.Pokemon()
            {
                Name = pokemonApi.Name,
                Category = pokemonSpeciesApi.Genera.FirstOrDefault(g => g.Language.Name == "en")?.Genus,
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
    }
}