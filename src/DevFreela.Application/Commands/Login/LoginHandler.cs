using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;

namespace DevFreela.Application.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginViewModel>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        var hashedPassword = _authService.ComputeHash(request.Password);

        if (user is null || !user.CheckPassword(hashedPassword))
        {
            return ResultViewModel<LoginViewModel>.Error("E-mail e/ou senha inválido(s)");
        }

        var token = _authService.GenerateToken(user.Email, user.Role);
        var loginViewModel = new LoginViewModel(token);

        return ResultViewModel<LoginViewModel>.Success(loginViewModel);
    }
}
