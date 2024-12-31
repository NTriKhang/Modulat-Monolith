using Evently.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Infrastructure.Interceptors
{
    public class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData
            , int result
            , CancellationToken cancellationToken = default)
        {
            if(eventData.Context is not null)
            {
                await PublishDomainEvent(eventData.Context);
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        private async Task PublishDomainEvent(DbContext dbContext)
        {
            var domainEvents = dbContext.ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .SelectMany(x =>
                {
                    IReadOnlyCollection<IDomainEvent> domainEvents = x.DomainEvents;
                    x.ClearDomainEvent();

                    return domainEvents;
                })
                .ToList();

            foreach(IDomainEvent domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }
}
