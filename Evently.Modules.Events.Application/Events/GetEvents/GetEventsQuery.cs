using Evently.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evently.Modules.Events.Application.Events.GetEvents
{
    public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<GetEventsResponseDto>?>;
}
