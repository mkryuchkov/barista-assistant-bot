using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using mkryuchkov.BaristaBot.TgBot.Interfaces;

namespace mkryuchkov.BaristaBot.TgBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBot(this IServiceCollection services)
        {
            services.AddScoped<IBotUserContext, BotUserContext>();

            services.AddAllImplementations(typeof(ITgPage));

            services.AddSingleton<TgPageLocator>(provider => name =>
                provider.GetServices<ITgPage>().First(s => s.GetType().Name == name));

            services.AddSingleton<IBot, Bot>();

            return services;
        }

        private static IServiceCollection AddAllImplementations(
            this IServiceCollection services,
            Type type,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var implementations = Assembly.GetExecutingAssembly().GetTypes().Where(x =>
                x.IsClass && !x.IsAbstract && type.IsAssignableFrom(x));

            foreach (var impl in implementations)
            {
                services.Add(new ServiceDescriptor(type, impl, lifetime));
            }

            return services;
        }
    }
}