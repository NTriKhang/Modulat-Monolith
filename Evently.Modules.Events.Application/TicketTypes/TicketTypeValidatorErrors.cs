using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.TicketTypes
{
    internal static class TicketTypeValidatorErrors
    {
        public static string TicketTypeIdNotNull { get; } = "Ticket type identifier can't be empty";
        public static string EventIdNotNull { get; } = "Event identifier can't be empty";
        public static string NameNotNull { get; } = "Name cannot be empty";
        public static string PriceNotNull { get; } = "Price cannot be empty";
        public static string CurrencyNotNull { get; } = "Currency cannot be empty";
        public static string QuantityNotNull { get; } = "Quantity cannot be empty";
        public static string PriceCannotBeNegative { get; } = "Price cannot be negative";
        public static string QuantityCannotBeNegative { get; } = "Quantity cannot be negative";
    }
}
