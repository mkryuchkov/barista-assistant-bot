using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot.Pages;

public class PageC : ITgPage
{
    public string Text => $"Text of {nameof(PageC)}.";

    public InlineKeyboardMarkup ReplyMarkup => new(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"nav:{nameof(PageA)}" },
        new InlineKeyboardButton("To B") { CallbackData = $"nav:{nameof(PageB)}" }
    });
}