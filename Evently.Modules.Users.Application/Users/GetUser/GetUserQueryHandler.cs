using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Domain.Users;
using System.Data.Common;

namespace Evently.Modules.Users.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 "Id" AS {nameof(UserResponse.Id)},
                 "Email" AS {nameof(UserResponse.Email)},
                 "FirstName" AS {nameof(UserResponse.FirstName)},
                 "LastName" AS {nameof(UserResponse.LastName)}
             FROM "user"."Users"
             WHERE "Id" = @UserId
             """;

            UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
            }

            return user;
        }
    }

}
