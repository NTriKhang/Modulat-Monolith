namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType
{
    public record CreateTicketTypeResponseDto(
        string Id
        , Guid EventId
        , string Name
        , decimal Price
        , string Currency
        , decimal Quantity);

}
