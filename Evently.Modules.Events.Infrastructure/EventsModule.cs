using Evently.Common.Application.Data;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Category;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Infrastructure.TicketType;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Common.Infrastructure.Interceptors;

namespace Evently.Modules.Events.Infrastructure
{
    public static class EventsModule
    {
        public static IServiceCollection AddEventsModule(
            this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            return services;
        }
        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string database = configuration.GetConnectionString("Database")!;

            services.AddDbContext<EventsDbContext>((sp, options) =>
                options.UseNpgsql(database,
                    npgsqlOptionsAction => npgsqlOptionsAction.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events)
                )
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .LogTo(Console.WriteLine)
            );
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<EventsDbContext>());
        }
    }
}
