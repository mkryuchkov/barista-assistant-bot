using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace mkryuchkov.BaristaBot.TgBot;

public class Bot : IBot
{
    private readonly ILogger<Bot> _logger;
    private readonly IDictionary<long, BotUserContext> _userContexts = new Dictionary<long, BotUserContext>();

    public Bot(ILogger<Bot> logger)
    {
        _logger = logger;
    }

    public Task InitUserAsync(Update update, CancellationToken token)
    {
        if (!_userContexts.ContainsKey(update.Message?.Chat.Id ?? default))
        {
            _userContexts[update.Message!.Chat.Id] = new BotUserContext
            {
                CurrentActivity = nameof(ActivityA)
            };
        }

        return Task.CompletedTask;
    }

    public async Task ProcessMessageAsync(Message message, CancellationToken token)
    {
        if (_userContexts.TryGetValue(message.Chat.Id, out var context))
        {
            // todo:
            // get activity
            // inject data context
            // render and send message
            // context.CurrentActivity;
        }
    }
}