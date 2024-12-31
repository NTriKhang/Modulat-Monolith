using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Users.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Presentation.Users
{
    internal class GetUserProfile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users/{id}/profile", async (Guid id, ISender sender) =>
            {
                Result<UserResponse> result = await sender.Send(new GetUserQuery(id));

                return result is null ? Results.NotFound() : Results.Ok(result.Value);
            })
            .WithTags(Tags.Users);
        }
    }
}
