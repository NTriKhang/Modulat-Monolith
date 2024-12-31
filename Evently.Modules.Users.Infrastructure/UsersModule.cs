using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Infrastructure
{
    public static class UsersModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services
            , IConfiguration configuration)
        {
            AddInfrastructure(services, configuration);

            return services;
        }
        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string database = configuration.GetConnectionString("Database")!;

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(database,
                    npgsqlOptionsAction => npgsqlOptionsAction.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.User)
                )
                .LogTo(Console.WriteLine)
            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<UserDbContext>());
        }
    }
}
