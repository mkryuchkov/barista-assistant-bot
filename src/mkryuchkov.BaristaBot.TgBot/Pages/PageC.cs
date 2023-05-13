using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Pages;

public class PageC : ITgPage
{
    public string Text => $"Text of {nameof(PageC)}.";

    public InlineKeyboardMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"navigate:{nameof(PageA)}" },
        new InlineKeyboardButton("To B") { CallbackData = $"navigate:{nameof(PageB)}" }
    });
}