using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.ToEntity();

        await _userRepository.CreateAsync(user);

        return ResultViewModel<int>.Success(user.Id);
    }
}
