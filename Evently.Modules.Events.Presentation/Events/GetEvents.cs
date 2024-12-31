using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.GetEvent;
using Evently.Modules.Events.Application.Events.GetEvents;
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
    internal  class GetEvents : IEndpoint
    {
        public  void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("events", async (ISender sender) =>
            {
                GetEventsQuery getEventQuery = new GetEventsQuery();

                Result<IReadOnlyCollection<GetEventsResponseDto>?> @event = await sender.Send(getEventQuery);

                return @event is null ? Results.NotFound() : Results.Ok(@event.Value);
            })
            .WithTags(Tags.Events);
        }
    }
}
