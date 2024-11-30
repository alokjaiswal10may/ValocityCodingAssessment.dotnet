using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingAssessment.Refactor
{
    /// <summary>
    /// Class represent people having name and DateOfBirth.
    /// </summary>
    public class People
    {
        public string Name { get; private set; }
        public DateTimeOffset DateOfBirth { get; private set; }

        /// <summary>
        /// Creates people with name and date of birth.
        /// </summary>
        /// <param name="name">The name of people.</param>
        /// <param name="dob">The date of birth of people.</param>
        public People(string name, DateTimeOffset dob)
        {
            // Refactor: Null validation check for the name parameter.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            // Refactor: Future date Validate check for date of birth.
            if (dob > DateTimeOffset.UtcNow)
            throw new ArgumentException("Date of birth cannot be in the future", nameof(dob));    

            Name = name;
            DateOfBirth = dob;
        }
    }

    /// <summary>
    /// Class resposible for creating and managing people data.
    /// </summary>
    public class BirthingUnit
    {
        // Constants added for MaxNameLength.
        private const int MaxNameLength = 255;

        // Changed from a private list to readonly.
        private readonly List<People> _people;

        /// <summary>
        /// Initializes a new instance of BirthingUnit.
        /// </summary>
        public BirthingUnit()
        {
            _people = new List<People>();
        }

        /// <summary>
        /// Generates a specified number of people with random values.
        /// </summary>
        /// <param name="count">The number of people to generate.</param>
        /// <returns>Read-only list of generated people.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when count is non-positive.</exception>
        public IReadOnlyList<People> GeneratePeople(int count)
        {
            // Refactor: Added validation to prevent invalid inputs.
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero.");

            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                // Refactor: Simplified random name generation logic.
                string name = random.Next(0, 2) == 0 ? "Bob" : "Betty";
                int randomAge = random.Next(18, 85); // Age between 18 and 85.
                var dob = DateTimeOffset.UtcNow.AddYears(-randomAge);

                _people.Add(new People(name, dob));
            }

            // Return as read-only.
            return _people.AsReadOnly();
        }

        /// <summary>
        /// Retrieves all people named "Bob" who are older than the specified age.
        /// </summary>
        /// <param name="age">Age for comparison.</param>
        /// <returns>An enumerable of people named "Bob" older than the specified age.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when age is negative.</exception>
        public IEnumerable<People> GetBobsOlderThan(int age)
        {
            // Refactor: Added validation for negative age.
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age), "Age must be non-negative.");

            // Calculate the date cutoff based on the specified age.
            DateTimeOffset ageCutoff = DateTimeOffset.UtcNow.AddYears(-age);

            // Refactor: Improved LINQ query to get bob older than specific age.
            return _people.Where(people => people.Name == "Bob" && people.DateOfBirth <= ageCutoff);
        }

        /// <summary>
        /// Concatenate a people's name with last name to generate a married name.
        /// </summary>
        /// <param name="people">People object whose name is used.</param>
        /// <param name="lastName">Last name to concatenate.</param>
        /// <returns>Full married name, truncated if it exceeds the maximum length.</returns>
        /// <exception cref="ArgumentNullException">Thrown when people is null.</exception>
        /// <exception cref="ArgumentException">Thrown when lastName is invalid.</exception>
        public string GenerateMarriedName(People people, string lastName)
        {
            // Refactor: Added Null validation for people.
            if (people == null)
                throw new ArgumentNullException(nameof(people));

            // Refactor: Added null empty validation for lastName. 
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            // Removed "test" check as it looks like added for testing purpose.

            // Combine names and truncate if necessary.
            string fullName = $"{people.Name} {lastName}";
            return fullName.Length > MaxNameLength ? fullName.Substring(0, MaxNameLength) : fullName;
        }
    }
}
