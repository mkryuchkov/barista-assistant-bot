using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace mkryuchkov.BaristaBot.TgBot.Extensions;

public static class UpdateExtensions
{
    public static bool TryGetChatId(this Update update, [NotNullWhen(true)] out long? chatId)
    {
        chatId = update.Type switch
        {
            UpdateType.MyChatMember => update.MyChatMember?.Chat.Id,
            UpdateType.Message => update.Message?.Chat.Id,
            UpdateType.CallbackQuery => update.CallbackQuery?.Message?.Chat.Id,
            _ => null
        };

        return chatId is not null;
    }
}