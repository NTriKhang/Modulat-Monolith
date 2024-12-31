using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Categories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Categories.UpdateCategory
{
    internal sealed class UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository
        , IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateCategoryCommand>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

            if (category is null)
            {
                return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
            }

            category.ChangeName(request.Name);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
