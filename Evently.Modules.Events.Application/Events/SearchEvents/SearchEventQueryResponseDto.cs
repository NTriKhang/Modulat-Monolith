using Evently.Modules.Events.Application.Events.GetEvents;

namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    public record SearchEventQueryResponseDto (
        int Page
        , int PageSize
        , int TotalCount
        , IReadOnlyCollection<GetEventsResponseDto> GetEventsResponseDtos);
}
