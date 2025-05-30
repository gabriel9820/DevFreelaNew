using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public CreateUserHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _authService.ComputeHash(request.Password);
        request.Password = hashedPassword;

        var user = request.ToEntity();

        await _userRepository.CreateAsync(user);

        return ResultViewModel<int>.Success(user.Id);
    }
}
