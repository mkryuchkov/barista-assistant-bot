using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Pages;

public class PageB : ITgPage
{
    public string Text => $"Text of {nameof(PageB)}.";

    public InlineKeyboardMarkup ReplyMarkup => new(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"nav:{nameof(PageA)}" },
        new InlineKeyboardButton("To C") { CallbackData = $"nav:{nameof(PageC)}" }
    });
}