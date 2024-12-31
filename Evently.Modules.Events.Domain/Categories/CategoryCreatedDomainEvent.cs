using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Categories
{
    internal sealed class CategoryCreatedDomainEvent(Guid categoryId) : DomainEvent
    {
        public Guid CategoryId { get; init; } = categoryId;
    }
}
