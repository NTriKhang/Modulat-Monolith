using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.Categories.GetCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Categories.GetCategories
{
    public sealed record GetCategoriesQuery : IQuery<IReadOnlyCollection<CategoryResponse>>;

}
