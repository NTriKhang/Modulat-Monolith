using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
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
    internal class UpdateTicketTypePrice : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("ticketTypes/{ticketTypeId}/price", async (Guid ticketTypeId, UpdateTicketTypePriceDto request, ISender sender) =>
            {
                UpdateTicketTypePriceCommand command = new UpdateTicketTypePriceCommand(ticketTypeId, request.Price);

                Result<Guid> res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
    internal sealed class UpdateTicketTypePriceDto
    {
        public decimal Price { get; init; }
    }
}
