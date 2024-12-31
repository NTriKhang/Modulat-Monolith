using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent
{
    public sealed record RescheduleEventDto(
        Guid EventId,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc
    );
}
