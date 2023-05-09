using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public interface IActivity<TDataContext> : ITelegramMessage
{
    string Name { get; }
}

public abstract class ActivityBase<TDataContext> : IActivity<TDataContext>
    where TDataContext: class // IBindable
{
    protected readonly TDataContext DataContext;

    protected ActivityBase(TDataContext dataContext)
    {
        DataContext = dataContext;
    }

    public abstract string Name { get; }

    public abstract string Text { get; }

    public abstract IReplyMarkup? ReplyMarkup { get; }
}