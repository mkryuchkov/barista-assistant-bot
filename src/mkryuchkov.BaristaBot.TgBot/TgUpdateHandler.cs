using Microsoft.Extensions.Logging;
using mkryuchkov.TgBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public class TgUpdateHandler : ITgUpdateHandler
{
    private readonly ILogger<TgUpdateHandler> _logger;
    private readonly ITelegramBotClient _botClient;

    public TgUpdateHandler(
        ILogger<TgUpdateHandler> logger,
        ITelegramBotClient botClient)
    {
        _logger = logger; 
        _botClient = botClient;
    }

    public async Task Handle(Update update, CancellationToken token)
    {
        _logger.LogInformation("Update from: {Username}",
            update.Message?.From?.Username);

        if (update.Message?.Text != null)
        {
            await _botClient.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Text: {update.Message.Text}",
                cancellationToken: token
            );

            await _botClient.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Text: {update.Message.Text}",
                replyMarkup: new InlineKeyboardMarkup(new []
                {
                    new InlineKeyboardButton("Button A") { CallbackData = "b.a" },
                    new InlineKeyboardButton("Button B") { CallbackData = "b.b" }
                }),
                cancellationToken: token
            );
        }
    }
}