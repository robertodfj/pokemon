namespace Pokemon.dto
{
    public class CreatePokemonDTO
    {
        public int PokemonApiId { get; set; }
        public string Name { get; set; }
        public bool IsShiny { get; set; }
        public int Level { get; set; }
    }
}