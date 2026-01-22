namespace PokemonMinimalAPI.PokemonController;
using Microsoft.AspNetCore.Mvc;
using PokemonMinimalAPI.PokemonPractical;
using PokemonMinimalAPI.Services.PokemonServices;
using PokemonMinimalAPI.DTOs.CreatePokemonDto;
using PokemonMinimalAPI.DTOs.PokemonResponseDto;
using Microsoft.AspNetCore.Authorization;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<PokemonResponseDto>> GetPokemons()
    {
        var pokemons = _pokemonService.GetAll();
        var response = pokemons.Select(p => new PokemonResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Level = p.Level
        });
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpGet("{name}")]
    public ActionResult<PokemonResponseDto> GetPokemonByName(string name)
    {
        var pokemon = _pokemonService.GetByName(name);
        if (pokemon == null)
        {
            return NotFound();
        }
        return Ok(new PokemonResponseDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level
        });
    }
    [HttpPost]
    public ActionResult<PokemonResponseDto> CreatePokemon(CreatePokemonDto dto)
    {
        var pokemon = new PokemonPractical
        {
            Name = dto.Name!
        };
        _pokemonService.Add(pokemon);
        var response = new PokemonResponseDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Level = pokemon.Level
        };
        return Ok(response);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{name}")]
    public ActionResult DeletePokemon(string name)
    {
        var pokemon = _pokemonService.Delete(name);

        if (!pokemon)
        {
            return NotFound();
        }
        return Ok();
    }
    [HttpPost("{name}/train/{amount}")]
    public ActionResult<PokemonPractical> TrainPokemon(string name, int amount)
    {
        var pokemon = _pokemonService.Train(name, amount);
        if (pokemon == null)
        {
            return NotFound();
        }
        return Ok(pokemon);
    }
} 