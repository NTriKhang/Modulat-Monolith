using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.TicketType
{
    internal class CreateTicketType : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("ticketTypes", async (CreateTicketTypeDto request, ISender sender) =>
            {
                CreateTicketTypeCommand command = new CreateTicketTypeCommand(request);

                Result<Guid> res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
}
