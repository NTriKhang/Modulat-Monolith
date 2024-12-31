using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Modules.Events.Domain.Events;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    internal class SearchEventQueryHandler(
        IDbConnectionFactory dbConnectionFactory) : IQueryHandler<SearchEventQuery, SearchEventQueryResponseDto>
    {
        public async Task<Result<SearchEventQueryResponseDto>> Handle(SearchEventQuery request, CancellationToken cancellationToken)
        {
            DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();

            SearchEventsParameters parameters = new SearchEventsParameters(
                (int)EventStatus.Published
                , request.Search.StartsAt?.Date
                , request.Search.EndsAt?.Date
                , request.Search.PageSize
                , (request.Search.Page - 1) * request.Search.PageSize);

            IReadOnlyCollection<GetEventsResponseDto> events = await GetEventsAsync(dbConnection, parameters);
            int totalCount = await CountEventsAsync(dbConnection, parameters);

            return new SearchEventQueryResponseDto(request.Search.Page, request.Search.PageSize, totalCount, events);
        }
        private static async Task<IReadOnlyCollection<GetEventsResponseDto>> GetEventsAsync(
            DbConnection connection,
            SearchEventsParameters parameters)
        {
           const string sql =
           $"""
           SELECT
               "Id" AS "{nameof(GetEventsResponseDto.Id)}",
               "Title" AS "{nameof(GetEventsResponseDto.Title)}",
               "Description" AS "{nameof(GetEventsResponseDto.Description)}",
               "Location" AS "{nameof(GetEventsResponseDto.Location)}",
               "StartsAtUtc" AS "{nameof(GetEventsResponseDto.StartsAtUtc)}",
               "EndsAtUtc" AS "{nameof(GetEventsResponseDto.EndsAtUtc)}",
               "Status" AS "{nameof(GetEventsResponseDto.Status)}"
           FROM "events"."Events"
           WHERE
               "Status" = @Status AND
               (@StartDate::timestamp IS NULL OR "StartsAtUtc" >= @StartDate::timestamp) AND
               (@EndDate::timestamp IS NULL OR "EndsAtUtc" >= @EndDate::timestamp)
           ORDER BY "StartsAtUtc"
           OFFSET @Skip
           LIMIT @Take
           """;

            List<GetEventsResponseDto> events = (await connection.QueryAsync<GetEventsResponseDto>(sql, parameters)).AsList();

            return events;
        }
        private static async Task<int> CountEventsAsync(DbConnection connection, SearchEventsParameters parameters)
        {
            const string sql =
            """
            SELECT COUNT(*)
            FROM "events"."Events"
            WHERE
            "Status" = @Status AND
            (@StartDate::timestamp IS NULL OR "StartsAtUtc" >= @StartDate::timestamp) AND
            (@EndDate::timestamp IS NULL OR "EndsAtUtc" >= @EndDate::timestamp)
            """;

            int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

            return totalCount;
        }
        private sealed record SearchEventsParameters(
            int Status,
            //Guid? CategoryId,
            DateTime? StartDate,
            DateTime? EndDate,
            int Take,
            int Skip);
    }
}
