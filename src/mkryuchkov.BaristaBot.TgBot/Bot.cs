using Microsoft.Extensions.Logging;
using mkryuchkov.BaristaBot.TgBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot;

public class Bot : IBot
{
    private readonly ILogger<Bot> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly IChatContext _context;

    public Bot(
        ILogger<Bot> logger,
        ITelegramBotClient botClient,
        IChatContext context)
    {
        _logger = logger;
        _botClient = botClient;
        _context = context;
    }

    public Task InitUserAsync(CancellationToken token)
    {
        _logger.LogDebug("Init chat {Id}", _context.ChatId);
        return Task.CompletedTask;
    }

    public async Task ProcessMessageAsync(Message message, CancellationToken token)
    {
        _logger.LogDebug("Process message from chat {Id}", message.Chat.Id);
        
        var page = _context.GetPage();

        _logger.LogDebug("Page {Page} located for chat {Id}",
            page.GetType().Name, message.Chat.Id);

        await _botClient.SendTextMessageAsync(
            message.Chat.Id,
            page.Text,
            replyMarkup: page.ReplyMarkup,
            cancellationToken: token
        );
    }

    public async Task ProcessCallbackAsync(CallbackQuery callback, CancellationToken token)
    {
        _logger.LogDebug("Process callback from chat {Id}", callback.Message?.Chat.Id);

        if (callback.Message != null)
        {
            if (callback.Data?.StartsWith("nav") ?? false)
            {
                var newPageName = callback.Data.Split(":")[1];

                if (newPageName == _context.PageName)
                {
                    _logger.LogDebug("No navigation needed");
                    return;
                }

                _context.PageName = newPageName;
            }

            var page = _context.GetPage();

            _logger.LogDebug("Page {Page} located for chat {Id}",
                page.GetType().Name, callback.Message.Chat.Id);

            await _botClient.EditMessageTextAsync(
                callback.Message!.Chat.Id,
                callback.Message!.MessageId,
                text: page.Text,
                replyMarkup: page.ReplyMarkup,
                cancellationToken: token);
        }
        else
        {
            _logger.LogError("Callback {Id} message is empty", callback.Id);
        }
    }
}