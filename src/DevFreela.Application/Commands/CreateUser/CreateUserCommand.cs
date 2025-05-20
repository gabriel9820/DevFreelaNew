using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<ResultViewModel<int>>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public User ToEntity()
    {
        return new User(FullName, Email, BirthDate);
    }
}
