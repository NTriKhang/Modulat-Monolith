using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Events.CreateEvent
{

    internal sealed class CreateEventCommandHandler(IEventRepository eventRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<CreateEventCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = EventEntity.Create(
                request.eventCreateDto.CategoryId
                , request.eventCreateDto.Title
                , request.eventCreateDto.Description
                , request.eventCreateDto.Location
                , request.eventCreateDto.StartsAtUtc
                , request.eventCreateDto.EndsAtUtc);

            if (@event.IsFailure)
            {
                return Result.Failure<Guid>(@event.Error);
            }
            eventRepository.Insert(@event.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return @event.Value.Id;
        }
    }
}

