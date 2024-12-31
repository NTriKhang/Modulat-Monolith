using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Domain.Users
{
    public sealed class User : Common.Domain.Entity
    {
        private User()
        {
        }
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public static User Create(Guid id, string email, string firstName, string lastName)
        {
            var user = new User();
            user.Id = id;
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;

            user.Raise(new UserRegisterdDomainEvent(user.Id));

            return user;
        }
        public void Update(string firstName, string lastName)
        {
            if(firstName == FirstName && lastName == LastName)
            {
                return;
            }
            FirstName = firstName;
            LastName = lastName;

            Raise(new UserProfileUpdateDomainEvent(Id, FirstName, LastName));
        }
    }
}
