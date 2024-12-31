using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Categories.UpdateCategory;
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
    internal class UpdateCategory : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("categories/{id}", async (Guid id, UpdateCategoryDto request, ISender sender) =>
            {
                Result res = await sender.Send(new UpdateCategoryCommand(id, request.Name));

                return res.IsSuccess ? Results.Ok(res) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Categories);
        }
        internal sealed class UpdateCategoryDto
        {
            public string Name { get; init; }
        }
    }
}
