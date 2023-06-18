using System.Collections.Concurrent;
using mkryuchkov.BaristaBot.TgBot.Interfaces;
using mkryuchkov.BaristaBot.TgBot.Pages;

namespace mkryuchkov.BaristaBot.TgBot;

public interface IChatContext
{
    long ChatId { get; set; }

    string PageName { get; set; } // todo: may be private + navigation methods

    ITgPage GetPage();

    long? LastMessageId { get; set; }

    IDictionary<string, object> Storage { get; }
}

public class ChatContext : IChatContext
{
    private readonly TgPageLocator _pageLocator;
    private const string DefaultPage = nameof(PageA); // todo: from config
    private readonly ConcurrentDictionary<string, object> _storage = new(); // todo: external persisted storage
    // todo: pour context from storage on init // setting ChatId

    public ChatContext(TgPageLocator pageLocator)
    {
        _pageLocator = pageLocator;
    }

    public long ChatId { get; set; }
    public string PageName { get; set; } = DefaultPage; // todo: get from storage
    public ITgPage GetPage() => _pageLocator(PageName);
    public long? LastMessageId { get; set; } = null;
    public IDictionary<string, object> Storage => _storage;
}