using Evently.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Domain.Users
{
    public sealed class UserErrors
    {
        public static Error NotFound(Guid userId) =>
            Error.NotFound("Users.NotFound", $"The user with the identifier {userId} not found");

        public static Error NotFound(string identityId) =>
            Error.NotFound("Users.NotFound", $"The user with the IDP identifier {identityId} not found");
    }
}
