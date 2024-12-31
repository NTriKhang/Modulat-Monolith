using Evently.Api.Extensions;
using Evently.Api.Middlewares;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Common.Presentation;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Users.Infrastructure;
using Evently.Modules.Users.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(); 

        builder.Services.AddInfrastructure(
            builder.Configuration?.GetConnectionString("Database") ?? "",
            builder.Configuration?.GetConnectionString("Cache") ?? "",
            [Evently.Modules.Events.Application.AssemblyReference.assembly
            , Evently.Modules.Users.Application.AssemblyReference.Assembly]);

        builder.Services.AddEventsModule(builder.Configuration!); 
        builder.Services.AddUsersModule(builder.Configuration!);    

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.ApplyMigration();
        }
        EndpointExtensions.MapEndpoint(app, [Evently.Modules.Events.Presentation.AssemblyReference.Assembly
            , Evently.Modules.Users.Presentation.AssemblyReference.Assembly]);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}