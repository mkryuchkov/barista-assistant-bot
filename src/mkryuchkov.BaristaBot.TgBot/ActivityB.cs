using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public class ActivityB : ActivityBase<ExampleModel>
{
    public ActivityB(ExampleModel dataContext) : base(dataContext)
    {
    }

    public override string Name => nameof(ActivityB);
    public override string Text => $"Text of {nameof(ActivityB)}: {DataContext.Content}.";

    public override IReplyMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"navigate:{nameof(ActivityA)}" },
        new InlineKeyboardButton("To C") { CallbackData = $"navigate:{nameof(ActivityC)}" }
    });
}