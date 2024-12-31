using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
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
    internal class RescheduleEvent : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("events/rescheduleEvent", async (RescheduleEventDto request, ISender sender) =>
            {
                RescheduleEventCommand command = new RescheduleEventCommand(request);

                Result res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(request) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Events);
        }
    }
}
