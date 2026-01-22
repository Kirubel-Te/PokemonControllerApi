namespace PokemonMinimalAPI.PokemonPractical
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    public class PokemonPractical
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Level { get; set; } = 1;

        public PokemonPractical(){}
        public PokemonPractical(string name)
        {
            Name = name;
        }
        public event Action<string,int> LeveledUp;

        public void GainExperience(int gain)
        {
            Level += gain;

            LeveledUp?.Invoke(Name, gain);
        }
    }
}