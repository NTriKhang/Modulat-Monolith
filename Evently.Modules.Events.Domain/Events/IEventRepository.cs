using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.Events
{
    public  interface IEventRepository
    {
        Task<EventEntity?> GetAsync(Guid EventId, CancellationToken cancellationToken = default);
        void Insert(EventEntity @event);
    }
}
