using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.PasswordRecoveryChange;

public class PasswordRecoveryChangeHandler : IRequestHandler<PasswordRecoveryChangeCommand, ResultViewModel>
{
    private readonly IMemoryCache _cacheService;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public PasswordRecoveryChangeHandler(IMemoryCache cacheService, IUserRepository userRepository, IAuthService authService)
    {
        _cacheService = cacheService;
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ResultViewModel> Handle(PasswordRecoveryChangeCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"PasswordRecoveryCode-{request.Email}";

        if (!_cacheService.TryGetValue(cacheKey, out string? code) || code != request.Code)
        {
            return ResultViewModel.Error("");
        }

        _cacheService.Remove(cacheKey);

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return ResultViewModel.Error("");
        }

        var hashedPassword = _authService.ComputeHash(request.NewPassword);
        user.UpdatePassword(hashedPassword);

        await _userRepository.UpdateAsync();

        return ResultViewModel.Success();
    }
}
