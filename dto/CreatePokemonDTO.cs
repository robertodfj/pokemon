using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto
{
    public class CreatePokemonDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PokemonApiId must be a positive integer.")]
        public int PokemonApiId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; set; }

        public bool IsShiny { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Level must be between 1 and 100.")]
        public int Level { get; set; }
    }
}