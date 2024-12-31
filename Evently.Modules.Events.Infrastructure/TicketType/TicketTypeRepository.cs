using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Infrastructure.TicketType
{
    internal class TicketTypeRepository(EventsDbContext dbContext) : ITicketTypeRepository
    {
        public async Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            return await dbContext.TicketTypes.AnyAsync(x => x.EventId == eventId, cancellationToken);
        }

        public async Task<Domain.TicketTypes.TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.TicketTypes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Insert(Domain.TicketTypes.TicketType tickType)
        {
            dbContext.TicketTypes.Add(tickType);
        }
    }
}
