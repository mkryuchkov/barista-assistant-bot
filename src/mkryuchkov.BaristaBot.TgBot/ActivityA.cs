using Telegram.Bot.Types.ReplyMarkups;

namespace mkryuchkov.BaristaBot.TgBot;

public class ActivityA : ActivityBase<ExampleModel>
{
    public ActivityA(ExampleModel dataContext) : base(dataContext)
    {
    }

    public override string Name => nameof(ActivityA);
    public override string Text => $"Text of {nameof(ActivityA)}: {DataContext.Content}.";

    public override IReplyMarkup ReplyMarkup => new InlineKeyboardMarkup(new[]
    {
        new InlineKeyboardButton("To B") { CallbackData = $"navigate:{nameof(ActivityB)}" },
        new InlineKeyboardButton("To C") { CallbackData = $"navigate:{nameof(ActivityC)}" }
    });
}