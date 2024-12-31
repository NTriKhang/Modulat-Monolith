using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Users.Infrastructure.Users
{
    internal sealed class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public void Insert(User user)
        {
            dbContext.Users.Add(user);
        }
    }
}
