using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Infrastructure.Category
{
    internal class CategoryRepository(EventsDbContext eventsDbContext) : ICategoryRepository
    {
        public async Task<Domain.Categories.Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await eventsDbContext.Categories.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Insert(Domain.Categories.Category category)
        {
            eventsDbContext.Categories.Add(category);
        }
    }
}
