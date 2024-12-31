using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Domain.Events
{
    public sealed partial class EventEntity : Entity
    {
        public EventEntity()
        {
            
        }
        public Guid Id { get; private set; }
        [ForeignKey(nameof(CategoryId))]
        public Guid? CategoryId { get; private set; }
        //public Category? Category { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartsAtUtc { get; private set; }
        public DateTime? EndsAtUtc { get; private set; }
        public EventStatus Status { get; private set; }

        public static Result<EventEntity> Create(
            Guid CategoryId,
            string Title,
            string Description,
            string Location,
            DateTime StartsAtUtc,
            DateTime? EndsAtUtc)
        {
            if (EndsAtUtc.HasValue && EndsAtUtc < StartsAtUtc)
            {
                return Result.Failure<EventEntity>(EventErrors.EndDatePrecedesStartDate);
            }

            var @event = new EventEntity()
            {
                Id = Guid.NewGuid(),
                CategoryId = CategoryId,
                Title = Title,
                Description = Description,
                Location = Location,
                StartsAtUtc = StartsAtUtc,
                EndsAtUtc = EndsAtUtc,
                Status = EventStatus.Draft,
            };

            @event.Raise(new EventCreatedDomainEvent(@event.Id));

            return @event;
        }
        public Result Publish()
        {
            if (Status != EventStatus.Draft)
            {
                return Result.Failure(EventErrors.NotDraft);
            }

            Status = EventStatus.Published;

            Raise(new EventPublishedDomainEvent(Id));

            return Result.Success();
        }
        public Result Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
        {
            if(startsAtUtc < DateTime.UtcNow)
            {
                return Result.Failure(EventErrors.StartDateInPast);
            }
            StartsAtUtc = startsAtUtc;
            EndsAtUtc = endsAtUtc;
            Raise(new EventRescheduledDomainEvent(Id, StartsAtUtc, EndsAtUtc));
            return Result.Success();
        }
        public Result Cancel()
        {
            if (Status == EventStatus.Canceled)
                return Result.Failure(EventErrors.AlreadyCanceled);
            if(StartsAtUtc <= DateTime.UtcNow)
                return Result.Failure(EventErrors.AlreadyStarted);

            Status = EventStatus.Canceled;
            Raise(new EventCanceledDomainEvent(Id));

            return Result.Success();
        }
    }
}
