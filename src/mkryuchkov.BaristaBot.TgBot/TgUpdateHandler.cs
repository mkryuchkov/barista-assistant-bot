using Microsoft.Extensions.Logging;
using mkryuchkov.TgBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public class TgUpdateHandler : ITgUpdateHandler
{
    private readonly ILogger<TgUpdateHandler> _logger;
    private readonly IBot _bot;
    private readonly ITelegramBotClient _botClient;

    public TgUpdateHandler(
        ILogger<TgUpdateHandler> logger,
        IBot bot,
        ITelegramBotClient botClient)
    {
        _logger = logger;
        _bot = bot;
        _botClient = botClient;
    }

    public async Task Handle(Update update, CancellationToken token)
    {
        _logger.LogInformation("Update from: {Username}",
            update.Message?.From?.Username);

        switch (update.Type)
        {
            case UpdateType.Message:

                if (update.Message!.Text == "/start")
                {
                    await _bot.InitUserAsync(update, token);
                }

                await _bot.ProcessMessageAsync(update.Message, token);
                //
                // await _botClient.SendTextMessageAsync(
                //     update.Message!.Chat.Id,
                //     $"Text: {update.Message.Text}",
                //     replyMarkup: new InlineKeyboardMarkup(new[]
                //     {
                //         new InlineKeyboardButton("Button A") { CallbackData = "b.a" },
                //         new InlineKeyboardButton("Button B") { CallbackData = "b.b" }
                //     }),
                //     cancellationToken: token
                // );
                break;
            case UpdateType.CallbackQuery:
                await _botClient.EditMessageTextAsync(
                    update.CallbackQuery!.Message!.Chat.Id,
                    update.CallbackQuery!.Message!.MessageId,
                    text: update.CallbackQuery!.Message.Text!,
                    cancellationToken: token);

                await _botClient.SendTextMessageAsync(
                    update.CallbackQuery!.Message!.Chat.Id,
                    $"Callback: {update.CallbackQuery.Data}",
                    cancellationToken: token
                );
                break;
            // case UpdateType.InlineQuery:
            // case UpdateType.ChosenInlineResult:
            // case UpdateType.EditedMessage:
            default:
                throw new InvalidOperationException("Unknown update type");
        }
    }
}