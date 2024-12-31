using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.CreateEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.Events
{
    internal class CreateEvent : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("events", async (CreateEventDto request, ISender sender) =>
            {
                CreateEventCommand command = new CreateEventCommand(request);

                Result<Guid> res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Events);
        }
    }
}

