using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Interfaces;

public interface ITgPage
{
    string Text { get; }
    InlineKeyboardMarkup ReplyMarkup { get; }
}