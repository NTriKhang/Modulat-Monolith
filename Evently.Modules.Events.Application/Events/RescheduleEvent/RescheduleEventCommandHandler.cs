using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using FluentValidation;

namespace Evently.Modules.Events.Application.Events.RescheduleEvent
{
    internal sealed class RescheduleEventCommandHandler(
        IEventRepository eventRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<RescheduleEventCommand>
    {
        public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
        {

            EventEntity? @event = await eventRepository.GetAsync(request.reschedule.EventId);

            if (@event is null)
                return Result.Failure(EventErrors.NotFound(request.reschedule.EventId));

            Result res = @event.Reschedule(request.reschedule.StartsAtUtc, request.reschedule.EndsAtUtc);

            if(!res.IsFailure)
                await unitOfWork.SaveChangesAsync(cancellationToken);

            return res;
        }
    }
}
