using Evently.Common.Application.Messaging;
using MediatR;

namespace Evently.Modules.Events.Application.Events.CreateEvent
{
    public sealed record CreateEventCommand(CreateEventDto eventCreateDto) : ICommand<Guid>;
}

