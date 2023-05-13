using Microsoft.Extensions.Logging;
using mkryuchkov.BaristaBot.TgBot.Interfaces;
using mkryuchkov.BaristaBot.TgBot.Pages;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot;

public class Bot : IBot
{
    private const string DefaultPage = nameof(PageA);
    private readonly ILogger<Bot> _logger;
    private readonly ITelegramBotClient _botClient;
    private readonly TgPageLocator _pageLocatorLocator;
    private readonly IDictionary<long, BotUserContext> _userContexts = new Dictionary<long, BotUserContext>();

    public Bot(
        ILogger<Bot> logger,
        ITelegramBotClient botClient,
        TgPageLocator pageLocatorLocator)
    {
        _logger = logger;
        _botClient = botClient;
        _pageLocatorLocator = pageLocatorLocator;
    }

    public Task InitUserAsync(long chatId, CancellationToken token)
    {
        _logger.LogDebug("Init chat {Id}", chatId);

        // todo: concurrent ready
        _userContexts[chatId] = new BotUserContext
        {
            CurrentPage = DefaultPage
        };

        return Task.CompletedTask;
    }

    public async Task ProcessMessageAsync(Message message, CancellationToken token)
    {
        _logger.LogDebug("Process message from chat {Id}", message.Chat.Id);

        if (_userContexts.TryGetValue(message.Chat.Id, out var context))
        {
            var page = _pageLocatorLocator(context.CurrentPage ?? DefaultPage);

            _logger.LogDebug("Page {Page} located for chat {Id}",
                page.GetType().Name, message.Chat.Id);

            await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                page.Text,
                replyMarkup: page.ReplyMarkup,
                cancellationToken: token
            );
        }
        else
        {
            _logger.LogError("No context for {Id}", message.Chat.Id);
        }
    }

    public async Task ProcessCallbackAsync(CallbackQuery callback, CancellationToken token)
    {
        _logger.LogDebug("Process callback from chat {Id}", callback.Message?.Chat.Id);

        if (callback.Message != null && _userContexts.TryGetValue(callback.Message.Chat.Id, out var context))
        {
            if (callback.Data?.StartsWith("navigate") ?? false)
            {
                var newPage = callback.Data.Split(":")[1];

                if (newPage == context.CurrentPage)
                {
                    _logger.LogDebug("No navigation needed");
                    return;
                }

                context.CurrentPage = newPage;
            }

            var page = _pageLocatorLocator(context.CurrentPage ?? DefaultPage);

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
            _logger.LogError("No context for {Id}", callback.Message?.Chat.Id);
        }
    }
}