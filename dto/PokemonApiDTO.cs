namespace Pokemon.dto
{
    public class PokemonApiDTO
    {
        public required string Name { get; set; }

        public required SpritesDTO Sprites { get; set; }
    }

    public class SpritesDTO
    {
        public required string Front_Default { get; set; }
        public required string Front_Shiny { get; set; }
    }
}
