namespace PokemonMinimalAPI.DTOs.PokemonResponseDto;

public class PokemonResponseDto
{
    public string Id {get; set; } = null!;

    public string Name { get; set; } = null!;
    public int Level { get; set; }
}