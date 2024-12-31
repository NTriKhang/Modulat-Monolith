using Evently.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice
{
    public record UpdateTicketTypePriceCommand(Guid TicketTypeId, decimal newPrice) : ICommand<Guid>;
}
