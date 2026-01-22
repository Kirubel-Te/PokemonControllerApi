namespace PokemonMinimalAPI.Trainer
{


public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string City { get; set; } = null!;
    public List<string> PokemonNames { get; set; } = new();
}

}