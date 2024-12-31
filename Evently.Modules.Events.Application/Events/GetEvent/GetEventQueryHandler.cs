using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Domain.TicketTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Events.GetEvent
{
    internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetEventQuery, GetEventResponseDto?>
    {
        public async Task<Result<GetEventResponseDto>?> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection dbConnection = await dbConnectionFactory.OpenConnectionAsync();
            const string sql =
                $"""
                   SELECT
                       e."Id" AS {nameof(GetEventResponseDto.Id)},
                       e."CategoryId" AS {nameof(GetEventResponseDto.CategoryId)},
                       e."Title" AS {nameof(GetEventResponseDto.Title)},
                       e."Description" AS {nameof(GetEventResponseDto.Description)},
                       e."Location" AS {nameof(GetEventResponseDto.Location)},
                       e."StartsAtUtc" AS {nameof(GetEventResponseDto.StartsAtUtc)},
                       e."EndsAtUtc" AS {nameof(GetEventResponseDto.EndsAtUtc)},
                       e."Status" AS {nameof(GetEventResponseDto.Status)},
                       tt."Id" AS {nameof(GetEventResponseDto.TicketTypeId)},
                       tt."Name" AS {nameof(GetEventResponseDto.Name)},
                       tt."Price" AS {nameof(GetEventResponseDto.Price)},
                       tt."Currency" AS {nameof(GetEventResponseDto.Currency)},
                       tt."Quantity" AS {nameof(GetEventResponseDto.Quantity)}
                   FROM "events"."Events" e
                   LEFT JOIN "events"."TicketTypes" tt ON tt."{nameof(TicketType.EventId)}" = e."Id"
                   WHERE e."Id" = @EventId
                """;

            return await dbConnection.QuerySingleOrDefaultAsync<GetEventResponseDto>(sql, request);

        }
    }
}
