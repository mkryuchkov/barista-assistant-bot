using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot.Interfaces;

public interface IBot
{
    Task InitUserAsync(CancellationToken token);
    Task ProcessMessageAsync(Message message, CancellationToken token);
    Task ProcessCallbackAsync(CallbackQuery callback, CancellationToken token);
}