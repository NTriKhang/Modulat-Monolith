using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Events.GetEvent;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.GetEvents
{
    internal sealed class GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventsQuery, IReadOnlyCollection<GetEventsResponseDto>?>
    {
        public async Task<Result<IReadOnlyCollection<GetEventsResponseDto>?>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
                   SELECT
                       e."Id" AS {nameof(GetEventsResponseDto.Id)},
                       e."CategoryId" AS {nameof(GetEventsResponseDto.CategoryId)},
                       e."Title" AS {nameof(GetEventsResponseDto.Title)},
                       e."Description" AS {nameof(GetEventsResponseDto.Description)},
                       e."Location" AS {nameof(GetEventsResponseDto.Location)},
                       e."StartsAtUtc" AS {nameof(GetEventsResponseDto.StartsAtUtc)},
                       e."EndsAtUtc" AS {nameof(GetEventsResponseDto.EndsAtUtc)},
                       e."Status" AS {nameof(GetEventsResponseDto.Status)}
                   FROM "events"."Events" e
                """;

            return (await dbConnection.QueryAsync<GetEventsResponseDto>(sql, request)).AsList();
        }
    }
}
