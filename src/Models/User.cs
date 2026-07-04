using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DiscordHandler.Models;

public class User
{
    [BsonId]
    public string Id { get; set; } = string.Empty;

    public long CreatedAt { get; set; }
}
