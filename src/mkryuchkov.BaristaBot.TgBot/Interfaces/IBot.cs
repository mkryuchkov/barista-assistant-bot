using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot.Interfaces;

public interface IBot
{
    Task InitUserAsync(long chatId, CancellationToken token);
    Task ProcessMessageAsync(Message message, CancellationToken token);
    Task ProcessCallbackAsync(CallbackQuery callback, CancellationToken token);
}