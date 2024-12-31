using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using FluentValidation;

namespace Evently.Modules.Events.Application.Events.CancelEvent
{
    internal sealed class CancelEventCommandHandler(
        IEventRepository eventRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<CancelEventCommand>
    {
        public async Task<Result> Handle(CancelEventCommand request, CancellationToken cancellationToken)
        {
            EventEntity? @event = await eventRepository.GetAsync(request.EventId);
            if(@event is null)
            {
                return Result.Failure(EventErrors.NotFound(request.EventId));
            }
            Result res = @event.Cancel();

            if (res.IsFailure)
                return res;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return res;
        }
    }
}
