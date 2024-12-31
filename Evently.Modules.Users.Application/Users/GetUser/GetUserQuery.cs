using Evently.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Application.Users.GetUser
{
    public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
}
