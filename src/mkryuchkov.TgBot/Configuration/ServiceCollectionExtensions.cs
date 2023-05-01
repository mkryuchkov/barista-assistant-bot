using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace mkryuchkov.TgBot.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTgBot(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<BotConfig>(
                configuration.GetSection(nameof(BotConfig)));

            services.AddHostedService<ConfigureWebHook>();

            services.AddHttpClient(Const.TgWebHook)
                .AddTypedClient<ITelegramBotClient>(httpClient =>
                    new TelegramBotClient(
                        configuration[$"{nameof(BotConfig)}:Token"]
                        ?? throw new InvalidOperationException(
                            $"No {nameof(BotConfig)}.{nameof(BotConfig.Token)} found."),
                        httpClient));

            services
                .AddControllers()
                .AddNewtonsoftJson();

            return services;
        }
    }
}