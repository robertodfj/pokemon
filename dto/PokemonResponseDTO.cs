using System.ComponentModel.DataAnnotations;

namespace Pokemon.dto
{
    public class PokemonResponseDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; set; }

        [Required]
        public required string Category { get; set; }

        [Required]
        [Url(ErrorMessage = "ImageURL must be a valid URL.")]
        public required string ImageURL { get; set; }
        public bool IsShiny { get; set; }
        
        [Required]
        [Range(1, 100, ErrorMessage = "Level must be between 1º and 100.")]
        public int Level { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "OwnerId must be a positive integer.")]
        public int OwnerId { get; set; }
    }
}