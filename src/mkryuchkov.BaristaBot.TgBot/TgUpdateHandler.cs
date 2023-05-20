using Microsoft.Extensions.Logging;
using mkryuchkov.BaristaBot.TgBot.Extensions;
using mkryuchkov.BaristaBot.TgBot.Interfaces;
using mkryuchkov.TgBot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace mkryuchkov.BaristaBot.TgBot;

public class TgUpdateHandler : ITgUpdateHandler
{
    private readonly ILogger<TgUpdateHandler> _logger;
    private readonly IBot _bot;
    private readonly IChatContext _context;

    public TgUpdateHandler(
        ILogger<TgUpdateHandler> logger,
        IBot bot,
        IChatContext context)
    {
        _logger = logger;
        _bot = bot;
        _context = context;
    }

    public async Task Handle(Update update, CancellationToken token)
    {
        if (update.TryGetChatId(out var chatId))
        {
            _context.ChatId = chatId.Value;
        }
        
        _logger.LogInformation("Update {Type} from: {ChatId} {Username}",
            update.Type, chatId, update.Message?.From?.Username ?? string.Empty);

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
                    await _bot.InitUserAsync(token);
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