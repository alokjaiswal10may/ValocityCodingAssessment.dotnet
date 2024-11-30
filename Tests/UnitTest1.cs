using System;
using System.Linq;
using CodingAssessment.Refactor;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class BirthingUnitTests
    {
        [Fact]
        public void GeneratePeople_ShouldCreateExpectedNumberOfPeople()
        {
            var birthingUnit = new BirthingUnit();
            var people = birthingUnit.GeneratePeople(5);

            people.Should().HaveCount(5);
        }

        [Fact]
        public void GeneratePeople_ShouldThrowForInvalidCount()
        {
            var birthingUnit = new BirthingUnit();
            Action action = () => birthingUnit.GeneratePeople(-1);

            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("*Count must be greater than zero*");
        }

        [Fact]
        public void GetBobsOlderThan_ShouldReturnCorrectPeople()
        {
            var birthingUnit = new BirthingUnit();
            birthingUnit.GeneratePeople(10);
            var bobsOlderThan30 = birthingUnit.GetBobsOlderThan(30);

            bobsOlderThan30.Should().OnlyContain(person => person.Name == "Bob" && person.DateOfBirth <= DateTimeOffset.UtcNow.AddYears(-30));
        }

        [Fact]
        public void GenerateMarriedName_ShouldCreateValidFullName()
        {
            var person = new Person("Alice", DateTimeOffset.UtcNow.AddYears(-25));
            var birthingUnit = new BirthingUnit();
            string marriedName = birthingUnit.GenerateMarriedName(person, "Smith");

            marriedName.Should().Be("Alice Smith");
        }

        [Fact]
        public void GenerateMarriedName_ShouldTruncateLongNames()
        {
            var person = new Person("Alice", DateTimeOffset.UtcNow.AddYears(-25));
            var birthingUnit = new BirthingUnit();
            string lastName = new string('S', 260);
            string marriedName = birthingUnit.GenerateMarriedName(person, lastName);

            marriedName.Length.Should().Be(255);
        }

        [Fact]
        public void GenerateMarriedName_ShouldThrowForInvalidInput()
        {
            var birthingUnit = new BirthingUnit();

            Action action = () => birthingUnit.GenerateMarriedName(null, "Smith");
            action.Should().Throw<ArgumentNullException>();

            var person = new Person("Alice", DateTimeOffset.UtcNow.AddYears(-25));
            Action action2 = () => birthingUnit.GenerateMarriedName(person, null);
            action2.Should().Throw<ArgumentException>().WithMessage("*Last name cannot be null or empty*");
        }
    }
}
