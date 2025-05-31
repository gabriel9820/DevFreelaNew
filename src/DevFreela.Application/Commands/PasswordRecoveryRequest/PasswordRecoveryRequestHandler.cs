using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DevFreela.Application.Commands.PasswordRecoveryRequest;

public class PasswordRecoveryRequestHandler : IRequestHandler<PasswordRecoveryRequestCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _cacheService;
    private readonly IEmailService _emailService;

    public PasswordRecoveryRequestHandler(IUserRepository userRepository, IMemoryCache cacheService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _cacheService = cacheService;
        _emailService = emailService;
    }

    public async Task<ResultViewModel> Handle(PasswordRecoveryRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return ResultViewModel.Success(); // Do not reveal if the email exists for security reasons
        }

        var code = new Random().Next(100000, 999999).ToString();
        var cacheKey = $"PasswordRecoveryCode-{request.Email}";

        _cacheService.Set(cacheKey, code, TimeSpan.FromMinutes(30));

        await _emailService.SendAsync(request.Email, "Código de recuperação de senha", $"Seu código de recuperação é: {code}");

        return ResultViewModel.Success();
    }
}
