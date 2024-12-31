using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Categories.GetCategories;
using Evently.Modules.Events.Application.Categories.GetCategory;
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
    internal class GetCategories : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("categories", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<CategoryResponse>> res = await sender.Send(new GetCategoriesQuery());

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Categories);
        }
    }
}
