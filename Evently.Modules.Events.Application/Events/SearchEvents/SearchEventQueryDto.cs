namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    public record SearchEventQueryDto(
        DateTime? StartsAt
        , DateTime? EndsAt
        , int Page
        , int PageSize);
}
