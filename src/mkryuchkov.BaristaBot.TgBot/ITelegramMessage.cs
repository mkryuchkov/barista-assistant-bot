using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public interface ITelegramMessage
{
    string Text { get; }
    IReplyMarkup? ReplyMarkup { get; }
}

public abstract class ActivityBase<TContext> : ITelegramMessage
    where TContext: class // IBindable
{
    public abstract string Name { get; }

    protected TContext Context;

    protected ActivityBase(TContext context)
    {
        Context = context;
    }

    string ITelegramMessage.Text { get; } = string.Empty;

    IReplyMarkup? ITelegramMessage.ReplyMarkup { get; } = null;
}

