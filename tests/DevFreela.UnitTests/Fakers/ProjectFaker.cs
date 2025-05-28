using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Fakers;

public class ProjectFaker : Faker<Project>
{
    public ProjectFaker()
    {
        CustomInstantiator(f => new Project(
            f.Commerce.ProductName(),
            f.Lorem.Sentence(),
            f.Random.Int(1, 50),
            f.Random.Int(1, 50),
            f.Finance.Amount(100, 10000, 2)
        ));

        RuleFor(p => p.Id, f => f.Random.Int(1, 100));
    }
}
