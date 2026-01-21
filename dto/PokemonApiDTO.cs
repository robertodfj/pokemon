namespace Pokemon.dto
{
    public class PokemonApiDTO
    {
        public string Name { get; set; }

        public SpritesDTO Sprites { get; set; }
    }

    public class SpritesDTO
    {
        public string Front_Default { get; set; }
        public string Front_Shiny { get; set; }
    }
}
