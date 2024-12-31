namespace Evently.Modules.Users.Domain.Users
{
    public sealed class UserRegisterdDomainEvent(Guid userId) : Common.Domain.DomainEvent
    {
        public Guid UserId { get; init; } = userId;
    }
}
