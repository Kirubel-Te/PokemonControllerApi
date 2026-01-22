using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using PokemonMinimalAPI.Trainer;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private static List<Trainer> trainers = new List<Trainer>();

    // POST: api/trainers
    [HttpPost]
    public ActionResult<Trainer> Create(Trainer trainer)
    {
        trainers.Add(trainer);
        return Ok(trainer);
    }

    // GET: api/trainers
    [HttpGet]
    public ActionResult<List<Trainer>> GetAll()
    {
        return Ok(trainers);
    }

    // GET: api/trainers/{id}
    [HttpGet("{id}")]
    public ActionResult<Trainer> GetById(int id)
    {
        var trainer = trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null)
            return NotFound();

        return Ok(trainer);
    }

    // POST: api/trainers/{id}/pokemon/{pokemonName}
    [HttpPost("{id}/pokemon/{pokemonName}")]
    public ActionResult<Trainer> AssignPokemon(int id, string pokemonName)
    {
        var trainer = trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null)
            return NotFound();

        if (!trainer.PokemonNames.Contains(pokemonName))
        {
            trainer.PokemonNames.Add(pokemonName);
        }

        return Ok(trainer);
    }
}
