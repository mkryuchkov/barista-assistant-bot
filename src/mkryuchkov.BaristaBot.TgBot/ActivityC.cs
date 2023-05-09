using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public class ActivityC : ActivityBase<ExampleModel>
{
    public ActivityC(ExampleModel dataContext) : base(dataContext)
    {
    }

    public override string Name => nameof(ActivityC);
    public override string Text => $"Text of {nameof(ActivityC)}: {DataContext.Content}.";

    public override IReplyMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To A") { CallbackData = $"navigate:{nameof(ActivityA)}" },
        new InlineKeyboardButton("To B") { CallbackData = $"navigate:{nameof(ActivityB)}" }
    });
}