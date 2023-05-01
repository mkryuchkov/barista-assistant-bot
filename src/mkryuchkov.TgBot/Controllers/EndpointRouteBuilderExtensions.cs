using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using mkryuchkov.TgBot.Configuration;

namespace mkryuchkov.TgBot.Controllers
{
    public static class EndpointRouteBuilderExtensions
    {
        public static ControllerActionEndpointConventionBuilder MapWebhookRoute(
            this IEndpointRouteBuilder endpoints,
            IConfiguration configuration)
        {
            var token = configuration.GetValue<string>($"{nameof(BotConfig)}:{nameof(BotConfig.Token)}")
                        ?? throw new InvalidOperationException("No token found");
            return endpoints.MapControllerRoute(
                Const.TgWebHook,
                $"bot/{token}",
                new
                {
                    controller = Const.TgWebHook,
                    action = "Post"
                });
        }
    }
}