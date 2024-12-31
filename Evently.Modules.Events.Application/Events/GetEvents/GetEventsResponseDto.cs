using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.GetEvents
{
    public sealed record GetEventsResponseDto(
        Guid Id,
        Guid CategoryId,
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc,
        EventStatus Status);
}
