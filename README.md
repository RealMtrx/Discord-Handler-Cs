<div align="center">
  <h1>Discord Handler — C#</h1>
  <p><strong>A production-ready Discord bot framework built with Discord.Net v3 and MongoDB — slash commands, prefix commands, anti-crash, webhook logging, and a modular <code>src/</code> architecture.</strong></p>

  <p>
    <a href="https://github.com/RealMtrx/Discord-Handler-Cs/blob/main/LICENSE"><img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="License"></a>
    <a href="https://github.com/RealMtrx/Discord-Handler-Cs/releases"><img src="https://img.shields.io/badge/version-0.9.0--beta-yellow" alt="Version 0.9.0 Beta"></a>
    <a href="https://github.com/RealMtrx/Discord-Handler-Cs/stargazers"><img src="https://img.shields.io/github/stars/RealMtrx/Discord-Handler-Cs" alt="Stars"></a>
    <a href="https://github.com/RealMtrx/Discord-Handler-Cs/issues"><img src="https://img.shields.io/github/issues/RealMtrx/Discord-Handler-Cs" alt="Issues"></a>
    <a href="https://github.com/RealMtrx/Discord-Handler-Cs/network"><img src="https://img.shields.io/github/forks/RealMtrx/Discord-Handler-Cs" alt="Forks"></a>
    <a href="https://github.com/RealMtrx/Discord-Handler/graphs/contributors"><img src="https://img.shields.io/badge/ecosystem-26%20repos-brightgreen" alt="26 Repos"></a>
    <a href="https://discord.gg/0hu2"><img src="https://img.shields.io/badge/discord-0hu2-5865F2" alt="Discord"></a>
  </p>

  <br>

  <p>
    <a href="#-features">Features</a> •
    <a href="#-quick-start">Quick Start</a> •
    <a href="#-project-structure">Structure</a> •
    <a href="#-api-reference">API</a> •
    <a href="#-database-edition">SQL Edition</a> •
    <a href="#-related-repositories">Ecosystem</a>
  </p>
</div>

---

## Overview

Discord Handler C# is a production-ready Discord bot framework built on **Discord.Net v3** with **MongoDB** storage. It provides a complete foundation for building Discord bots with slash commands, prefix commands, event handling, anti-crash protection, and webhook-based logging — all organized in a clean, scalable `src/` directory structure.

> **Version:** 0.9.0 (Stable Beta) — Part of the [Discord Handler](https://github.com/RealMtrx/Discord-Handler) ecosystem (26 repos across 13 languages).

## Features

- **Dual Command System** — Slash commands and prefix commands via Discord.Net's `SocketSlashCommand` and `SocketMessage`
- **MongoDB Integration** — Persistent data storage with the official MongoDB.Driver
- **Modular Architecture** — Clean separation: Commands, Events, Handlers, Core, Database, Models
- **Anti-Crash Protection** — Handles `UnhandledException` and `UnobservedTaskException` with webhook reporting
- **Async Runtime** — Fully async event-driven architecture with .NET Task Parallel Library
- **Webhook Logging** — Dedicated webhooks for errors, slash/prefix commands, guild joins/leaves, and ready events via `HttpClient`
- **Cooldown System** — Per-command rate limiting using `ConcurrentDictionary`-based `CooldownManager`
- **Emoji System** — Centralized emoji constants via `Emojis` static class for consistent rendering
- **Environment Configuration** — Secure token and secrets management via `DotNetEnv`
- **Startup Report** — Terminal banner showing loaded commands, events, and MongoDB connection status
- **Graceful Shutdown** — Clean disconnect with `Task.Delay(-1)` / `CancellationToken`

## Quick Start

```bash
# Clone the repository
git clone https://github.com/RealMtrx/Discord-Handler-Cs.git
cd Discord-Handler-Cs

# Restore dependencies
dotnet restore

# Configure environment
cp .env.example .env
# Edit .env with your bot token, client ID, and MongoDB URI

# Run the bot
dotnet run
```

### Prerequisites

- **.NET 8.0+** — Runtime environment
- **MongoDB** — Local or Atlas instance
- **Discord Application** — Bot token and client ID from the [Discord Developer Portal](https://discord.com/developers/applications)

### Environment Variables

```env
TOKEN=your_bot_token
CLIENT_ID=your_client_id
BOT_NAME=Discord Handler
OWNER_IDS=owner_id_1,owner_id_2
PREFIX=$
MONGODB_URI=mongodb://localhost:27017/discord_bot
ERROR_WEBHOOK=your_webhook_url
SLASH_WEBHOOK=your_webhook_url
PREFIX_WEBHOOK=your_webhook_url
JOIN_WEBHOOK=your_webhook_url
LEAVE_WEBHOOK=your_webhook_url
READY_WEBHOOK=your_webhook_url
```

## Project Structure

```
Discord-Handler-Cs/
├── DiscordHandler.csproj          # .NET project file (net8.0)
├── .env.example                   # Environment template
├── LICENSE
├── src/
│   ├── Program.cs                 # Entry point — initializes everything
│   ├── Config.cs                  # BotConfig singleton loaded from env vars
│   ├── Bot.cs                     # Bot class with DiscordSocketClient, command maps
│   ├── Core/                      # Shared utilities
│   │   ├── CommandUtils.cs        # ErrorReport, FormatError, LogCommandUsage
│   │   ├── CooldownManager.cs     # ConcurrentDictionary-based cooldown tracker
│   │   ├── Emojis.cs              # Unicode emoji constants
│   │   ├── ErrorWebhook.cs        # Error reporting webhook
│   │   ├── JoinGuildWebhook.cs    # Guild join notification
│   │   ├── LeaveGuildWebhook.cs   # Guild leave notification
│   │   ├── PrefixCommandWebhook.cs
│   │   ├── ReadyWebhook.cs        # Bot ready event webhook
│   │   ├── SlashCommandWebhook.cs
│   │   └── Webhooks.cs            # WebhookEmbed, WebhookSender, shared helpers
│   ├── Database/
│   │   └── Mongo.cs               # MongoDB connection, ping, user CRUD
│   ├── Events/                    # Discord event handlers
│   │   ├── ErrorHandler.cs        # Discord error handler
│   │   ├── GuildCreateHandler.cs  # Guild join webhook
│   │   ├── GuildDeleteHandler.cs  # Guild leave webhook
│   │   ├── InteractionCreateHandler.cs # Slash command dispatch + cooldown
│   │   ├── MessageCreateHandler.cs     # Prefix command dispatch + cooldown
│   │   └── ReadyHandler.cs        # Bot ready — sets activity, sends webhook
│   ├── Handlers/                  # Loaders and registrars
│   │   ├── AntiCrash.cs           # AppDomain and TaskScheduler exception handlers
│   │   ├── CommandHandler.cs      # LoadSlashCommands — slash command loader
│   │   ├── EventHandler.cs        # LoadEvents — event module loader
│   │   ├── Logger.cs              # PrintStartupBanner
│   │   └── PrefixHandler.cs       # LoadPrefixCommands — prefix command loader
│   ├── Models/
│   │   ├── EventFile.cs           # EventFile data model
│   │   ├── PrefixCommand.cs       # PrefixCommand data model
│   │   ├── SlashCommand.cs        # SlashCommand data model
│   │   ├── StartupData.cs         # Startup report data model
│   │   └── User.cs                # MongoDB user model (BsonId)
│   └── Commands/
│       ├── Slash/
│       │   └── Public/
│       │       └── Ping.cs        # Shows WebSocket and API latency
│       └── Prefix/
│           └── Public/
│               └── Ping.cs        # Shows message and API latency
```

## API Reference

### Core Types

| Type | Location | Description |
| ---- | -------- | ----------- |
| `BotConfig` | `Config.cs` | Singleton configuration loaded from environment variables |
| `Bot` | `Bot.cs` | Main bot class with `DiscordSocketClient`, config, and command maps |
| `SlashCommand` | `Models/SlashCommand.cs` | Slash command metadata model |
| `PrefixCommand` | `Models/PrefixCommand.cs` | Prefix command metadata model |
| `CooldownManager` | `Core/CooldownManager.cs` | Thread-safe per-command cooldown tracker |
| `WebhookEmbed` | `Core/Webhooks.cs` | Discord embed class with `ToDict()` for webhook payloads |
| `User` | `Models/User.cs` | MongoDB user model with `BsonId` attribute |

### Core Functions

| Function | Location | Description |
| -------- | -------- | ----------- |
| `BotConfig.Load()` | `Config.cs` | Loads env vars into `BotConfig.Instance` |
| `new Bot()` | `Bot.cs` | Creates a new Bot with configured `DiscordSocketClient` |
| `await bot.StartAsync()` | `Bot.cs` | Logs in and starts the Discord client |
| `cd.Check(userId, command, ms)` | `Core/CooldownManager.cs` | Returns `(OnCooldown, RemainingSeconds)` |
| `WebhookSender.SendWebhook(url, embed)` | `Core/Webhooks.cs` | Posts an embed to a Discord webhook |
| `FormatError(error, commandName)` | `Core/CommandUtils.cs` | Returns an `ErrorReport` |
| `LogCommandUsage(userId, userName, cmd, guild)` | `Core/CommandUtils.cs` | Prints command usage to stdout |

### Event Handlers

| Handler | File | Description |
| ------- | ---- | ----------- |
| `ReadyHandler` | `Events/ReadyHandler.cs` | Sets bot activity, sends ready webhook |
| `InteractionCreateHandler` | `Events/InteractionCreateHandler.cs` | Routes slash commands with cooldown + webhook |
| `MessageCreateHandler` | `Events/MessageCreateHandler.cs` | Routes prefix commands with cooldown + webhook |
| `GuildCreateHandler` | `Events/GuildCreateHandler.cs` | Sends guild join notification |
| `GuildDeleteHandler` | `Events/GuildDeleteHandler.cs` | Sends guild leave notification |
| `ErrorHandler` | `Events/ErrorHandler.cs` | Sends error to webhook |

### Handlers

| Function | Location | Description |
| -------- | -------- | ----------- |
| `AntiCrash.Setup()` | `Handlers/AntiCrash.cs` | Attaches global exception handlers |
| `EventHandler.LoadEvents(bot)` | `Handlers/EventHandler.cs` | Scans and registers event modules |
| `CommandHandler.LoadSlashCommands(bot)` | `Handlers/CommandHandler.cs` | Loads slash commands |
| `PrefixHandler.LoadPrefixCommands(bot)` | `Handlers/PrefixHandler.cs` | Loads prefix commands |
| `Logger.PrintStartupBanner(data, elapsed)` | `Handlers/Logger.cs` | Prints startup summary |

### Database

| Function | Location | Description |
| -------- | -------- | ----------- |
| `Mongo.ConnectAsync()` | `Database/Mongo.cs` | Connects with 10s timeout, pings database |
| `Mongo.GetUser(userId)` | `Database/Mongo.cs` | Fetches user by Id |
| `Mongo.CreateOrUpdateUser(userId, data)` | `Database/Mongo.cs` | Upserts user document |

## Adding Commands

### Slash Command

Create a new file in `src/Commands/Slash/[category]/[name].cs`:

```csharp
using Discord.WebSocket;

namespace DiscordHandler.Commands.Slash.Public;

public class HelloCommand
{
    public string Name => "hello";
    public string Description => "Say hello!";
    public string Category => "public";

    public async Task Execute(SocketSlashCommand command)
    {
        await command.RespondAsync("Hello! 👋");
    }
}
```

### Prefix Command

Create a new file in `src/Commands/Prefix/[category]/[name].cs`:

```csharp
using Discord.WebSocket;

namespace DiscordHandler.Commands.Prefix.Public;

public class HelloCommand
{
    public string Name => "hello";
    public string Description => "Say hello!";
    public string Category => "public";
    public string[] Aliases => ["hi"];

    public async Task Execute(SocketMessage message, string[] args)
    {
        await message.Channel.SendMessageAsync("Hello! 👋");
    }
}
```

## Database Edition

This is the **MongoDB edition**. A **SQL edition** using Sequelize ORM is also available:

| Feature | MongoDB Edition | SQL Edition |
| ------- | --------------- | ----------- |
| Repository | [Discord-Handler-Cs](https://github.com/RealMtrx/Discord-Handler-Cs) | [Discord-Handler-Cs-Sequelize](https://github.com/RealMtrx/Discord-Handler-Cs-Sequelize) |
| Database | MongoDB | SQLite, PostgreSQL, MySQL, MSSQL |
| Driver | MongoDB.Driver | Entity Framework Core |
| Dialects | MongoDB only | Multi-dialect via config |

## Related Repositories

Discord Handler C# is part of a **26-repo ecosystem**. Here are the other repositories:

### Core Framework (MongoDB)

| Language | Repository |
| -------- | ---------- |
| JavaScript | [Discord-Handler-Js](https://github.com/RealMtrx/Discord-Handler-Js) |
| TypeScript | [Discord-Handler-Ts](https://github.com/RealMtrx/Discord-Handler-Ts) |
| Go | [Discord-Handler-Go](https://github.com/RealMtrx/Discord-Handler-Go) |
| Rust | [Discord-Handler-Rs](https://github.com/RealMtrx/Discord-Handler-Rs) |
| Python | [Discord-Handler-Py](https://github.com/RealMtrx/Discord-Handler-Py) |
| Java | [Discord-Handler-Java](https://github.com/RealMtrx/Discord-Handler-Java) |
| Kotlin | [Discord-Handler-Kt](https://github.com/RealMtrx/Discord-Handler-Kt) |
| C++ | [Discord-Handler-Cpp](https://github.com/RealMtrx/Discord-Handler-Cpp) |
| Dart | [Discord-Handler-Dart](https://github.com/RealMtrx/Discord-Handler-Dart) |
| Ruby | [Discord-Handler-Rb](https://github.com/RealMtrx/Discord-Handler-Rb) |
| Lua | [Discord-Handler-Lua](https://github.com/RealMtrx/Discord-Handler-Lua) |
| PHP | [Discord-Handler-Php](https://github.com/RealMtrx/Discord-Handler-Php) |

### Database Editions (SQL)

| Language | Repository |
| -------- | ---------- |
| JavaScript | [Discord-Handler-Js-Sequelize](https://github.com/RealMtrx/Discord-Handler-Js-Sequelize) |
| TypeScript | [Discord-Handler-Ts-Sequelize](https://github.com/RealMtrx/Discord-Handler-Ts-Sequelize) |
| Go | [Discord-Handler-Go-Sequelize](https://github.com/RealMtrx/Discord-Handler-Go-Sequelize) |
| Rust | [Discord-Handler-Rs-Sequelize](https://github.com/RealMtrx/Discord-Handler-Rs-Sequelize) |
| Python | [Discord-Handler-Py-Sequelize](https://github.com/RealMtrx/Discord-Handler-Py-Sequelize) |
| C# | [Discord-Handler-Cs-Sequelize](https://github.com/RealMtrx/Discord-Handler-Cs-Sequelize) |
| Java | [Discord-Handler-Java-Sequelize](https://github.com/RealMtrx/Discord-Handler-Java-Sequelize) |
| Kotlin | [Discord-Handler-Kt-Sequelize](https://github.com/RealMtrx/Discord-Handler-Kt-Sequelize) |
| C++ | [Discord-Handler-Cpp-Sequelize](https://github.com/RealMtrx/Discord-Handler-Cpp-Sequelize) |
| Dart | [Discord-Handler-Dart-Sequelize](https://github.com/RealMtrx/Discord-Handler-Dart-Sequelize) |
| Ruby | [Discord-Handler-Rb-Sequelize](https://github.com/RealMtrx/Discord-Handler-Rb-Sequelize) |
| Lua | [Discord-Handler-Lua-Sequelize](https://github.com/RealMtrx/Discord-Handler-Lua-Sequelize) |
| PHP | [Discord-Handler-Php-Sequelize](https://github.com/RealMtrx/Discord-Handler-Php-Sequelize) |

### Hub

| Repository | Description |
| ---------- | ----------- |
| [Discord-Handler](https://github.com/RealMtrx/Discord-Handler) | Central hub — documentation, examples, changelog, roadmap |

## License

MIT License — Copyright © 2026 Mtrx

---

<div align="center">
  <sub>Built by <strong>Mtrx</strong> — Discord: <strong>0hu2</strong></sub>
  <br>
  <sub><a href="https://github.com/RealMtrx/Discord-Handler">Discord Handler Ecosystem</a></sub>
</div>
