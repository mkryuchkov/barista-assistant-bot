using mkryuchkov.BaristaBot.TgBot.Interfaces;

namespace mkryuchkov.BaristaBot.TgBot;

public class BotUserContext : IBotUserContext
{
    public string? CurrentPage { get; set; }
}