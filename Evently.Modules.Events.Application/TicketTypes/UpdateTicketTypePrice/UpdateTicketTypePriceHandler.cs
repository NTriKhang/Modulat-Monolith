using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.TicketTypes;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice
{
    internal class UpdateTicketTypePriceHandler(
        IUnitOfWork unitOfWork
        , ITicketTypeRepository ticketTypeRepository) : ICommandHandler<UpdateTicketTypePriceCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(UpdateTicketTypePriceCommand request, CancellationToken cancellationToken)
        {
            TicketType? ticketType = await ticketTypeRepository.GetAsync(request.TicketTypeId);
            if (ticketType == null)
            {
                return Result.Failure<Guid>(TicketTypeErrors.NotFound(request.TicketTypeId));
            }
            ticketType.UpdatePrice(request.newPrice);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ticketType.Id);
        }
    }
}
