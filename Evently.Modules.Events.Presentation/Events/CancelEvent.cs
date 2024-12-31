using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.CancelEvent;
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
    internal class CancelEvent : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("events/cancelEvent", async (Guid EventId, ISender sender) =>
            {
                CancelEventCommand command = new CancelEventCommand(EventId);

                Result res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(EventId) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Events);
        }
    }
}
