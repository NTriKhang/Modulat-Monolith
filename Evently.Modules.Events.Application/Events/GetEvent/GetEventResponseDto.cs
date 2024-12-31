using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvent
{
    public sealed record GetEventResponseDto(
        Guid Id,
        Guid CategoryId,
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc,
        EventStatus Status,
        Guid? TicketTypeId,
        string? Name,
        decimal? Price,
        string? Currency,
        decimal? Quantity);
}
