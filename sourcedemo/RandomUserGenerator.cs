using Bogus;

public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public required string ZipCode { get; set; }

}

public class RandomUserGenerator
{
    public Person GenerateRandomPerson()
    {
        var faker = new Faker<Person>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.DateOfBirth, f => f.Date.Past(30))
            .RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        return faker.Generate();
    }
}
