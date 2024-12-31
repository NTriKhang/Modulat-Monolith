using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
using Evently.Modules.Events.Application.Events.SearchEvents;
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
    internal class SearchEvent : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("events/search", async (ISender sender
                , DateTime? StartsAt
                , DateTime? EndsAt
                , int page = 1
                , int pageSize = 15) =>
            {
                SearchEventQueryDto request = new SearchEventQueryDto(StartsAt, EndsAt, page, pageSize);
                SearchEventQuery command = new SearchEventQuery(request);

                Result<SearchEventQueryResponseDto> res = await sender.Send(command);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Events);
        }
    }
}
