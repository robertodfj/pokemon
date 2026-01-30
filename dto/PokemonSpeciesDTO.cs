namespace Pokemon.dto
{
    public class PokemonSpeciesDTO
    {
        public required List<GeneraDTO> Genera { get; set; }
    }

    public class GeneraDTO
    {
        public required string Genus { get; set; }
        public required LanguageDTO Language { get; set; }
    }

    public class LanguageDTO
    {
        public required string Name { get; set; }
    }
}