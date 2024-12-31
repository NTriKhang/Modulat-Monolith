using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events;
using Evently.Modules.Events.Application.Events.GetEvent;
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
    internal  class GetEvent : IEndpoint
    {
        public  void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("events/{id}", async (Guid id, ISender sender) =>
            {
                Result<GetEventResponseDto>? @event = await sender.Send(new GetEventQuery(id));

                return @event is null ? Results.NotFound() : Results.Ok(@event.Value);
            })
            .WithTags(Tags.Events);
        }
    }
}
