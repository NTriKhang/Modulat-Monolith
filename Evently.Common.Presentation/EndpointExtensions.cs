using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Common.Presentation
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapEndpoint(this WebApplication app
            , Assembly[] assemblies)
        {
            var tmp = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x is { IsAbstract: false, IsInterface: false }
                        && x.IsAssignableTo(typeof(IEndpoint)));

            IEndpoint[] endpointList = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x is { IsAbstract: false, IsInterface: false }
                        && x.IsAssignableTo(typeof(IEndpoint)))
                .Select(x => (IEndpoint)Activator.CreateInstance(x)!).ToArray();

            foreach(IEndpoint endpoint in endpointList)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }
}
