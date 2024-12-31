using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Events.Application.Categories.ArchiveCategory;
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
    internal class ArchiveCategory : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("categories/{id}/archive", async (Guid id, ISender sender) =>
            {
                Result res = await sender.Send(new ArchiveCategoryCommand(id));

                return res.IsSuccess ? Results.Ok(res) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Categories);
        }
    }
}
