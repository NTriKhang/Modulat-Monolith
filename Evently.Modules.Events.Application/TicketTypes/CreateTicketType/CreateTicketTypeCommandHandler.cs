using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Domain.TicketTypes;
using FluentValidation;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType
{
    internal class CreateTicketTypeCommandHandler(
        IEventRepository eventRepository
        , ITicketTypeRepository ticketTypeRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<CreateTicketTypeCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
        {

            EventEntity? @event = await eventRepository.GetAsync(request.request.EventId, cancellationToken);
            if(@event == null)
            {
                return Result.Failure<Guid>(EventErrors.NotFound(request.request.EventId));
            }

            var ticketType = TicketType.Create(
                @event,
                request.request.Name,
                request.request.Price,
                request.request.Currency,
                request.request.Quantity);

            ticketTypeRepository.Insert(ticketType);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success<Guid>(ticketType.Id);
        }
    }
}
