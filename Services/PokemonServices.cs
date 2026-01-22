
namespace PokemonMinimalAPI.Services.PokemonServices
{
    using PokemonMinimalAPI.PokemonPractical;
    using MongoDB.Driver;
    using Microsoft.Extensions.Options;
    using PokemonMinimalAPI.DBSettings;
    public class PokemonService
    {
        private readonly IMongoCollection<PokemonPractical> _pokemonsCollection;

    public PokemonService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _pokemonsCollection = mongoDatabase.GetCollection<PokemonPractical>("Pokemons");
        }
    public List<PokemonPractical> GetAll()
        {
            return _pokemonsCollection.Find(_ => true).ToList();
        }

    public PokemonPractical GetByName(string name)
    {
        return _pokemonsCollection.Find(p => p.Name == name).FirstOrDefault();
    }

    public void Add(PokemonPractical pokemon)
    {
        _pokemonsCollection.InsertOne(pokemon);
    }

    public bool Delete(string name)
    {
        var result = _pokemonsCollection.DeleteOne(p => p.Name == name);
        return result.DeletedCount > 0;
    }

    public PokemonPractical Train(string name, int amount)
    {
        var pokemon = GetByName(name);
        if (pokemon != null)
        {
            pokemon.GainExperience(amount);
            _pokemonsCollection.ReplaceOne(p => p.Id == pokemon.Id, pokemon);
        }
        return pokemon;
    }
}}