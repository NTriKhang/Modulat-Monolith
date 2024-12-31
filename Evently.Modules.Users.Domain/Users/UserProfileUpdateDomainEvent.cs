namespace Evently.Modules.Users.Domain.Users
{
    public sealed class UserProfileUpdateDomainEvent(Guid userId
        , string firstName
        , string lastName) : Common.Domain.DomainEvent
    {
        public Guid UserId { get; init; } = userId;
        public string FirstName { get; init; } = firstName;
        public string LastName { get; init; } = lastName;
    }
}
