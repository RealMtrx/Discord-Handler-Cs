# Discord Handler C#

A modern, feature-rich Discord bot handler built with **Discord.Net v3**, featuring both slash commands and prefix commands with a robust modular architecture designed for scalability and maintainability.

## 🚀 Features

- **Dual Command System**: Support for both slash commands and prefix commands
- **Modular Architecture**: Clean separation of concerns with dedicated handlers
- **Anti-Crash System**: Comprehensive error handling and monitoring
- **Event-Driven**: Fully event-driven async architecture
- **Webhook Logging**: Real-time logging for errors and guild events
- **MongoDB Integration**: Persistent data storage with MongoDB.Driver
- **Cooldown System**: Per-command cooldown management
- **Environment Configuration**: Secure configuration with DotNetEnv

## 📁 Project Structure

```
Discord-Handler-Cs/
├── Discord-Handler-Cs.csproj     # .NET project file
├── src/                          # Source code
│   ├── Program.cs                # Main bot entry point
│   ├── Config.cs                 # Bot configuration from .env
│   ├── Bot.cs                    # Bot initialization
│   ├── Core/                     # Core utilities
│   │   ├── CommandUtils.cs       # Cooldown and utilities
│   │   ├── Emojis.cs             # Centralized emoji definitions
│   │   └── WebhookUtil.cs        # Webhook utility
│   ├── Database/
│   │   └── Mongo.cs              # MongoDB connection setup
│   ├── Events/                   # Discord event handlers
│   │   ├── GuildCreate.cs        # Handler when bot joins a server
│   │   ├── GuildDelete.cs        # Handler when bot leaves a server
│   │   ├── InteractionCreate.cs  # Handles slash command interactions
│   │   ├── MessageCreate.cs      # Handles prefix commands
│   │   └── Ready.cs              # Bot ready event
│   ├── Handlers/                 # Handlers for modularity
│   │   ├── AntiCrash.cs          # Crash prevention and error handling
│   │   └── Logger.cs             # Logger for bot activity
│   ├── Models/
│   │   └── UserModel.cs          # User data model
│   └── Commands/
│       ├── Prefix/               # Prefix commands
│       │   └── Ping.cs           # Example prefix ping command
│       └── Slash/                # Slash commands
│           └── Ping.cs           # Example slash ping command
```

## 🔧 Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/RealMtrx/Discord-Handler-Cs.git
   cd Discord-Handler-Cs
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Environment Setup**

   Copy `.env.example` to `.env` and fill in your values:

   ```env
   TOKEN=your_bot_token_here
   PREFIX=!
   BOT_NAME=Discord Handler
   MONGO_URI=mongodb://localhost:27017/discord-handler
   ERROR_WEBHOOK=https://discord.com/api/webhooks/your_webhook
   GUILD_LOG_WEBHOOK=https://discord.com/api/webhooks/your_webhook
   ```

4. **Run the bot**

   ```bash
   dotnet run
   ```

## 📋 Dependencies

- **Discord.Net**: v3.13 - Discord API wrapper
- **MongoDB.Driver**: v2.23 - MongoDB driver
- **DotNetEnv**: v2.5 - Environment variable management

## 📝 Command Development

### Creating Slash Commands

Create a new file in `src/Commands/Slash/[category]/[name].cs`:

```csharp
using Discord.Net;
using Discord.WebSocket;

public class PingCommand : SlashCommand
{
    public override string Name => "ping";
    public override string Description => "Replies with Pong!";

    public override async Task Execute(SocketSlashCommand command)
    {
        await command.RespondAsync("Pong! 🏓");
    }
}
```

### Creating Prefix Commands

Create a new file in `src/Commands/Prefix/[category]/[name].cs`:

```csharp
using Discord.WebSocket;

public class PingCommand : PrefixCommand
{
    public override string Name => "ping";

    public override async Task Execute(SocketMessage message, string[] args)
    {
        await message.Channel.SendMessageAsync("Pong! 🏓");
    }
}
```

---

**Discord Handler** - A modern, scalable Discord bot framework built with C#.
