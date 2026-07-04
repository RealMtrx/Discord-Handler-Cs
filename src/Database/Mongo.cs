using MongoDB.Driver;
using DiscordHandler.Config;
using DiscordHandler.Models;

namespace DiscordHandler.Database;

public static class Mongo
{
    private static IMongoClient? _client;
    private static IMongoDatabase? _db;

    public static async Task<bool> ConnectAsync()
    {
        var uri = BotConfig.Instance.MongoDbUri;
        if (string.IsNullOrEmpty(uri) || uri == "#")
        {
            Console.WriteLine("  \u274c MongoDB URI not configured, skipping");
            return false;
        }

        try
        {
            var settings = MongoClientSettings.FromConnectionString(uri);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);
            settings.ConnectTimeout = TimeSpan.FromSeconds(10);

            _client = new MongoClient(settings);
            _db = _client.GetDatabase("discord_bot");

            await _db.RunCommandAsync<MongoDB.Bson.BsonDocument>(new MongoDB.Bson.BsonDocument("ping", 1));
            Console.WriteLine("  \u2705 MongoDB connected successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  \u274c MongoDB connection failed: {ex.Message}");
            return false;
        }
    }

    public static async Task<User?> GetUser(string userId)
    {
        if (_db == null) return null;
        var collection = _db.GetCollection<User>("users");
        return await collection.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public static async Task CreateOrUpdateUser(string userId, User? data = null)
    {
        if (_db == null) return;
        var collection = _db.GetCollection<User>("users");
        var user = data ?? new User { Id = userId, CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() };
        await collection.UpdateOneAsync(
            u => u.Id == userId,
            Builders<User>.Update.SetOnInsert(u => u, user),
            new UpdateOptions { IsUpsert = true }
        );
    }
}
