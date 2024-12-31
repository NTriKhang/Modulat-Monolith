using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.Category
{
    internal  class CreateCategory : IEndpoint
    {
        public  void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("categories", async (CreateCategoryDto request, ISender sender) =>
            {
                Result<Guid> res = await sender.Send(new CreateCategoryCommand(request.Name));

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Categories);
        }
        internal sealed record CreateCategoryDto(string Name);

    }
}
