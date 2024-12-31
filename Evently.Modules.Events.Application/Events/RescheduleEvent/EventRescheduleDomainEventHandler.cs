using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evently.Modules.Events.Domain.Events;
using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent
{
    internal class EventRescheduleDomainEventHandler : IDomainEventHandler<EventRescheduledDomainEvent>
    {
        public Task Handle(EventRescheduledDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
