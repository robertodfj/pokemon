namespace Pokemon.model
{
    public class Pokemon
    {
        public int Id { get; set; }
        public int PokemonApiId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ImageURL { get; set; }
        public bool IsShiny { get; set; }
        public int Level { get; set; }
        public int OwnerId { get; set; }
        
    }
}