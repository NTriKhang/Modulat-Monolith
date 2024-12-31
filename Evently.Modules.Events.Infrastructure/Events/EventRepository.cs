using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Infrastructure.Events
{
    internal sealed class EventRepository(EventsDbContext context) : IEventRepository
    {
        public async Task<EventEntity?> GetAsync(Guid EventId, CancellationToken cancellationToken = default)
        {
            return await context.Events.SingleOrDefaultAsync(x => x.Id == EventId, cancellationToken);
        }
        public void Insert(EventEntity @event)
        {
            context.Events.Add(@event);
        }
    }
}
