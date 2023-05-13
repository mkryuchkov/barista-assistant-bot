using Microsoft.Extensions.Logging;
using mkryuchkov.BaristaBot.TgBot.Interfaces;
using mkryuchkov.TgBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
        _logger.LogInformation("Update {Type} from: {ChatId} {Username}",
            update.Type,
            update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id,
            update.Message?.From?.Username ?? string.Empty);

        switch (update.Type)
        {
            case UpdateType.MyChatMember:
                _logger.LogInformation("UpdateType.MyChatMember: {Type}, {ChatId}, {User}",
                    update.MyChatMember!.NewChatMember.Status,
                    update.MyChatMember.Chat.Id,
                    update.MyChatMember.NewChatMember.User.Username);

                break;

            case UpdateType.Message:
                if (update.Message!.Text == "/start")
                {
                    await _bot.InitUserAsync(update.Message.Chat.Id, token);
                }

                await _bot.ProcessMessageAsync(update.Message!, token);

                break;

            case UpdateType.CallbackQuery:
                
                await _bot.ProcessCallbackAsync(update.CallbackQuery!, token);
                
                break;
            // case UpdateType.InlineQuery:
            // case UpdateType.ChosenInlineResult:
            // case UpdateType.EditedMessage:
            default:
                _logger.LogWarning("Unknown update: {Type}",
                    update.Type);
                break;
        }
    }
}