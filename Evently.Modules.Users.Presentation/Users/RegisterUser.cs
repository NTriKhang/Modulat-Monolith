using Evently.Common.Domain;
using Evently.Common.Presentation;
using Evently.Modules.Users.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Presentation.Users
{
    internal class RegisterUser : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/register", async (RegisterUserDto request, ISender sender) =>
            {
                RegisterUserDto registerUserDto = new RegisterUserDto(
                request.Email
                , request.FirstName
                , request.LastName);
                RegisterUserCommand registerUserCommand = new RegisterUserCommand(registerUserDto);

                Result<Guid> res = await sender.Send(registerUserCommand);
                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }
                else
                {
                    return Results.Created($"/users/{res.Value}", res.Value);
                }
            })
            .AllowAnonymous()
            .WithTags(Tags.Users);
        }
    }
}
