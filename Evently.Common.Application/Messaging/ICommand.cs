using Evently.Common.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Application.Messaging
{
    public interface IBaseCommand;
    public interface ICommand : IRequest<Result>, IBaseCommand;
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;
}
