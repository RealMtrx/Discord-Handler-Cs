namespace DiscordHandler.Config;

public class BotConfig
{
    public static BotConfig Instance { get; private set; } = new();

    public string Token { get; private set; } = "#";
    public string ClientId { get; private set; } = "#";
    public string BotName { get; private set; } = "Discord Handler";
    public string Prefix { get; private set; } = "$";
    public List<string> OwnerIds { get; private set; } = [];
    public string MongoDbUri { get; private set; } = "#";
    public string ErrorWebhook { get; private set; } = "#";
    public string SlashCommandWebhook { get; private set; } = "#";
    public string PrefixCommandWebhook { get; private set; } = "#";
    public string JoinGuildWebhook { get; private set; } = "#";
    public string LeaveGuildWebhook { get; private set; } = "#";
    public string ReadyWebhook { get; private set; } = "#";

    public bool ErrorWebhookEnabled => ErrorWebhook != "#" && !string.IsNullOrEmpty(ErrorWebhook);
    public bool SlashCommandWebhookEnabled => SlashCommandWebhook != "#" && !string.IsNullOrEmpty(SlashCommandWebhook);
    public bool PrefixCommandWebhookEnabled => PrefixCommandWebhook != "#" && !string.IsNullOrEmpty(PrefixCommandWebhook);
    public bool JoinGuildWebhookEnabled => JoinGuildWebhook != "#" && !string.IsNullOrEmpty(JoinGuildWebhook);
    public bool LeaveGuildWebhookEnabled => LeaveGuildWebhook != "#" && !string.IsNullOrEmpty(LeaveGuildWebhook);
    public bool ReadyWebhookEnabled => ReadyWebhook != "#" && !string.IsNullOrEmpty(ReadyWebhook);

    public static void Load()
    {
        var config = new BotConfig
        {
            Token = Env("TOKEN", "#"),
            ClientId = Env("CLIENT_ID", "#"),
            BotName = Env("BOT_NAME", "Discord Handler"),
            Prefix = Env("PREFIX", "$"),
            OwnerIds = (Env("OWNER_IDS", "#,#") ?? "#,#").Split(',').Select(x => x.Trim()).ToList(),
            MongoDbUri = Env("MONGODB_URI", "#"),
            ErrorWebhook = Env("ERROR_WEBHOOK", "#"),
            SlashCommandWebhook = Env("SLASH_WEBHOOK", "#"),
            PrefixCommandWebhook = Env("PREFIX_WEBHOOK", "#"),
            JoinGuildWebhook = Env("JOIN_WEBHOOK", "#"),
            LeaveGuildWebhook = Env("LEAVE_WEBHOOK", "#"),
            ReadyWebhook = Env("READY_WEBHOOK", "#"),
        };

        Instance = config;
    }

    private static string Env(string key, string fallback)
    {
        return Environment.GetEnvironmentVariable(key) ?? fallback;
    }
}
