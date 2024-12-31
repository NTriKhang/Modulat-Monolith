namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType
{
    public record CreateTicketTypeDto(
        Guid EventId
        , string Name
        , decimal Price
        , string Currency
        , decimal Quantity);

}
