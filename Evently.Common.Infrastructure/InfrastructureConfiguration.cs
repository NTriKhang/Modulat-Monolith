using Evently.Common.Application.Behaviors;
using Evently.Common.Application.Caching;
using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Interceptors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services
            , string databaseConnectionString
            , string redisConnectionString
            , Assembly[] assemblies)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

            NpgsqlDataSource dataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
            services.TryAddSingleton(dataSource);
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            services.TryAddSingleton<ICacheService, CacheService>();

            services.TryAddSingleton<PublishDomainEventsInterceptor>();

            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(option =>
            {
                option.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer);
            });

            return services;
        }
    }
}
