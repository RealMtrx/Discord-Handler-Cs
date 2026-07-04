# Discord Handler (C#)

A modern, feature-rich Discord bot handler built with **C#** and **Discord.Net v3**, featuring both slash commands and prefix commands with a robust modular architecture designed for scalability and maintainability.

## Features

- **Dual Command System**: Support for both slash commands (`/ping`) and prefix commands (`$ping`)
- **Modular Architecture**: Clean separation of concerns with dedicated handlers
- **Anti-Crash System**: Comprehensive error handling and monitoring
- **Event-Driven**: Fully event-driven architecture
- **Async/Await**: Full asynchronous design with `Task`
- **Webhook Logging**: Real-time logging for errors, commands, guild events, and bot status
- **MongoDB Integration**: Persistent data storage with `MongoDB.Driver`
- **Cooldown System**: Per-command cooldown management
- **Environment Configuration**: Secure configuration management with `DotNetEnv`

## Project Structure

```
Discord-Handler-Csharp/
├── DiscordHandler.csproj         # Project file with dependencies
├── .env.example                  # Environment configuration template
├── .gitignore
├── LICENSE
├── README.md
└── src/
    ├── Program.cs                # Main bot entry point
    ├── Config.cs                 # Bot configuration from environment
    ├── Bot.cs                    # Bot class (wraps DiscordSocketClient)
    ├── Core/                     # Core utilities and webhooks
    │   ├── Emojis.cs
    │   ├── CooldownManager.cs
    │   ├── CommandUtils.cs
    │   ├── Webhooks.cs           # Webhook base types and sender
    │   ├── ErrorWebhook.cs
    │   ├── JoinGuildWebhook.cs
    │   ├── LeaveGuildWebhook.cs
    │   ├── PrefixCommandWebhook.cs
    │   ├── ReadyWebhook.cs
    │   └── SlashCommandWebhook.cs
    ├── Database/
    │   └── Mongo.cs              # MongoDB connection with MongoDB.Driver
    ├── Events/                   # Discord event handlers
    │   ├── ErrorHandler.cs
    │   ├── GuildCreateHandler.cs
    │   ├── GuildDeleteHandler.cs
    │   ├── InteractionCreateHandler.cs
    │   ├── MessageCreateHandler.cs
    │   └── ReadyHandler.cs
    ├── Handlers/                 # Loaders and registrars
    │   ├── AntiCrash.cs
    │   ├── CommandHandler.cs
    │   ├── EventHandler.cs
    │   ├── Logger.cs
    │   └── PrefixHandler.cs
    ├── Models/                   # Data models
    │   ├── StartupData.cs
    │   ├── SlashCommand.cs
    │   ├── PrefixCommand.cs
    │   ├── EventFile.cs
    │   └── User.cs
    └── Commands/
        ├── Slash/Public/Ping.cs
        └── Prefix/Public/Ping.cs
```

## Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/RealMtrx/Discord-Handler-Csharp.git
   cd Discord-Handler-Csharp
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Environment Setup**

   Copy `.env.example` to `.env` and fill in your values:

   ```env
   TOKEN=your_bot_token
   CLIENT_ID=your_client_id
   BOT_NAME=Discord Handler
   PREFIX=$
   OWNER_IDS=owner_id_1,owner_id_2
   MONGODB_URI=your_mongo_uri
   ERROR_WEBHOOK=#
   SLASH_WEBHOOK=#
   PREFIX_WEBHOOK=#
   JOIN_WEBHOOK=#
   LEAVE_WEBHOOK=#
   READY_WEBHOOK=#
   ```

4. **Run the bot**

   ```bash
   dotnet run
   ```

## Requirements

- **.NET SDK**: 8.0+
- **Discord.Net**: 3.16+ — Discord API wrapper
- **MongoDB.Driver**: 2+ — MongoDB driver
- **DotNetEnv**: 2+ — Environment variable management

## Command Development

### Creating Slash Commands

Create a new file in `src/Commands/Slash/[category]/[name].cs`:

```csharp
using Discord;
using Discord.WebSocket;
using DiscordHandler.Models;

namespace DiscordHandler.Commands.Slash.[Category];

public static class [CommandName]
{
    public static SlashCommand GetCommand()
    {
        var builder = new SlashCommandBuilder()
            .WithName("commandname")
            .WithDescription("Command description");

        return new SlashCommand
        {
            Name = "commandname",
            Data = builder.Build(),
            Category = "[category]",
            Handler = async (slash) =>
            {
                await slash.RespondAsync("Response");
            }
        };
    }
}
```

### Creating Prefix Commands

Create a new file in `src/Commands/Prefix/[category]/[name].cs`:

```csharp
using Discord;
using Discord.WebSocket;
using DiscordHandler.Models;

namespace DiscordHandler.Commands.Prefix.[Category];

public static class [CommandName]
{
    public static PrefixCommand GetCommand()
    {
        return new PrefixCommand
        {
            Name = "commandname",
            Description = "Command description",
            Category = "[category]",
            Aliases = ["cmd"],
            Handler = async (message, args) =>
            {
                await message.Channel.SendMessageAsync("Response");
            }
        };
    }
}
```

---

**Discord Handler** - A modern, scalable Discord bot framework built with C#.
