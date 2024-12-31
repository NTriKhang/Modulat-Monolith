using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.TicketTypes
{
    public class TicketType : Entity
    {
        public Guid Id { get; private set; }
        [ForeignKey(nameof(EventId))]
        public Guid? EventId { get; private set; }
        //public EventEntity? EventEntity { get; set; }
        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Currency { get; private set; }

        public decimal Quantity { get; private set; }
        public static TicketType Create(
             EventEntity @event,
             string name,
             decimal price,
             string currency,
             decimal quantity)
        {
            var ticketType = new TicketType
            {
                Id = Guid.NewGuid(),
                EventId = @event.Id,
                Name = name,
                Price = price,
                Currency = currency,
                Quantity = quantity
            };

            ticketType.Raise(new TicketTypeCreateDomainEvent(ticketType.Id));

            return ticketType;
        }
        public void UpdatePrice(decimal price)
        {
            if (Price == price)
            {
                return;
            }

            Price = price;

            Raise(new TicketTypePriceChangedDomainEvent(Id, Price));
        }
    }
}
