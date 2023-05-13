using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Pages;

public class PageA : ITgPage
{
    public string Text => $"Text of {nameof(PageA)}";

    public InlineKeyboardMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To B") { CallbackData = $"navigate:{nameof(PageB)}" },
        new InlineKeyboardButton("To C") { CallbackData = $"navigate:{nameof(PageC)}" }
    });
}