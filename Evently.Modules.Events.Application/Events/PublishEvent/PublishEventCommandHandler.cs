using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Application.Events.PublishEvent
{
    internal class PublishEventCommandHandler(
        IEventRepository eventRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<PublishEventCommand>
    {
        public async Task<Result> Handle(PublishEventCommand request, CancellationToken cancellationToken)
        {
            var @event = await eventRepository.GetAsync(request.EventId);

            if (@event is null)
                return Result.Failure(EventErrors.NotFound(request.EventId));

            Result res = @event.Publish();
            if(res.IsFailure)
                return res;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return res;
        }
    }
}
