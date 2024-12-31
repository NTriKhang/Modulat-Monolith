using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;

namespace Evently.Modules.Users.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler(
        IUnitOfWork unitOfWork
        , IUserRepository userRepository) : ICommandHandler<RegisterUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            User user = User.Create(
                Guid.NewGuid()
                , request.Register.Email
                , request.Register.FirstName
                , request.Register.LastName);

            userRepository.Insert(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
