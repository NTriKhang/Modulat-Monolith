using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
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
    internal class GetTicketTypes : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ticketTypes/{eventId}", async (Guid eventId, ISender sender) =>
            {
                GetTicketTypesQuery command = new GetTicketTypesQuery(eventId);

                Result<IReadOnlyCollection<TicketTypeResponse>> res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
}
