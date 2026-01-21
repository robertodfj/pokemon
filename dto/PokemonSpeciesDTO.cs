namespace Pokemon.dto
{
    public class PokemonSpeciesDTO
    {
        public List<GeneraDTO> Genera { get; set; }
    }

    public class GeneraDTO
    {
        public string Genus { get; set; }
        public LanguageDTO Language { get; set; }
    }

    public class LanguageDTO
    {
        public string Name { get; set; }
    }
}