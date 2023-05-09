using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace mkryuchkov.BaristaBot.TgBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBot(
            this IServiceCollection services)
        {
            services.AddImplementations(typeof(ActivityBase<>));

            services.AddSingleton<IBot, Bot>();

            return services;
        }

        public static IServiceCollection AddImplementations(
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