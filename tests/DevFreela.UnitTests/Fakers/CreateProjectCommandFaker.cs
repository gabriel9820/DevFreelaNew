using Bogus;
using DevFreela.Application.Commands.CreateProject;

namespace DevFreela.UnitTests.Fakers;

public class CreateProjectCommandFaker : Faker<CreateProjectCommand>
{
    public CreateProjectCommandFaker()
    {
        RuleFor(c => c.Title, f => f.Commerce.ProductName());
        RuleFor(c => c.Description, f => f.Lorem.Sentence());
        RuleFor(c => c.ClientId, f => f.Random.Int(1, 50));
        RuleFor(c => c.FreelancerId, f => f.Random.Int(1, 50));
        RuleFor(c => c.TotalCost, f => f.Finance.Amount(100, 10000, 2));
    }
}
