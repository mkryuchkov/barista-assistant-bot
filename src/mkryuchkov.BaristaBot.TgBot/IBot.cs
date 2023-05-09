using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot;

public interface IBot
{
    Task InitUserAsync(Update update, CancellationToken token);
    Task ProcessMessageAsync(Message message, CancellationToken token);
}