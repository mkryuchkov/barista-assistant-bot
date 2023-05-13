using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Pages;

public class PageB : ITgPage
{
    public string Text => $"Text of {nameof(PageB)}.";

    public InlineKeyboardMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"navigate:{nameof(PageA)}" },
        new InlineKeyboardButton("To C") { CallbackData = $"navigate:{nameof(PageC)}" }
    });
}