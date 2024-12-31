using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Application.Users
{
    internal static class UserValidatorErrors
    {
        public static string UserFirstNameNotNull { get; } = "User first name can't be empty";
        public static string UserLastNameNotNull { get; } = "User last name can't be empty";
        public static string UserEmailNotNull { get; } = "User email can't be empty";
        public static string UserEmailInvalid { get; } = "User email is invalid";
    }
}
