using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.PasswordRecoveryValidate;

public class PasswordRecoveryValidateHandler : IRequestHandler<PasswordRecoveryValidateCommand, ResultViewModel>
{
    private readonly IMemoryCache _cacheService;

    public PasswordRecoveryValidateHandler(IMemoryCache cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<ResultViewModel> Handle(PasswordRecoveryValidateCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"PasswordRecoveryCode-{request.Email}";

        if (!_cacheService.TryGetValue(cacheKey, out string? code) || code != request.Code)
        {
            return Task.FromResult(ResultViewModel.Error(""));
        }

        return Task.FromResult(ResultViewModel.Success());
    }
}
