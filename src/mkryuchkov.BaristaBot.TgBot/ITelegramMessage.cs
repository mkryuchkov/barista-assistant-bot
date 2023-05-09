using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public interface ITelegramMessage
{
    string Text { get; }
    IReplyMarkup? ReplyMarkup { get; }
}