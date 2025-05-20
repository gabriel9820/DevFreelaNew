using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommandBehavior : IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly IUserRepository _userRepository;

    public CreateProjectCommandBehavior(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel<int>> Handle(
        CreateProjectCommand request,
        RequestHandlerDelegate<ResultViewModel<int>> next,
        CancellationToken cancellationToken)
    {
        var clientExists = await _userRepository.ExistsAsync(request.ClientId);

        if (!clientExists)
        {
            return ResultViewModel<int>.Error("Cliente não existe");
        }

        var freelancerExists = await _userRepository.ExistsAsync(request.FreelancerId);

        if (!freelancerExists)
        {
            return ResultViewModel<int>.Error("Freelancer não existe");
        }

        return await next();
    }
}
