﻿using Evently.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.TicketTypes.CreateTicketType
{
    public record CreateTicketTypeCommand(CreateTicketTypeDto request) : ICommand<Guid>;
}
