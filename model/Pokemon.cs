namespace Pokemon.model
{
    public class Pokemon
    {
        public int Id { get; set; }
        public int PokemonApiId { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required string ImageURL { get; set; }
        public bool IsShiny { get; set; }
        public int Level { get; set; }
        public int OwnerId { get; set; }
        
    }
}