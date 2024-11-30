using System;
using System.Collegctions.Generic;
using System.Linq;

namespace Utility.Valocity.ProfileHelper
{
    public class People
    {
        // ❌ Naming: 'DefaultDOBUnder16' can be used as its gives in an idea that is an default age field. 
        private static readonly DateTimeOffset Under16 = DateTimeOffset.UtcNow.AddYears(-15);
        public string Name { get; private set; }
        public DateTimeOffset DOB { get; private set; }

        // ❌ Renaming : Rename it to DefaultDOBUnder16 
        public People(string name) : this(name, Under16.Date) { }

        // ❌ Add validation: 'name' and 'dob' validation can be added to check null/invalid values like :-
        // Exception 1 for Name cannot be null or empty.
        // Exception 2 Date of birth cannot be in the future.
        public People(string name, DateTime dob)
        {
            Name = name;
            DOB = dob;
        }
    }

    public class BirthingUnit
    {
        /// <summary>
        /// MaxItemsToRetrieve  // ❌ Misleading summary: can be updated to "Collection of people for BirthingUnit.".
        /// </summary>
        private List<People> _people;

        public BirthingUnit()
        {
            _people = new List<People>();
        }

        /// <summary>
        /// GetPeoples   //  ❌ Better summary : Generates a list of random people.
        /// </summary>
        /// <param name="j"></param>  // ❌ Incorrect parameter naming : It should be 'i' to match the parameter.
        /// <returns>List<object></returns>   // ❌ Incorrect return type : It should be List<People>.
        public List<People> GetPeople(int i)   // ❌ Can be renamed to GeneratePeople or CreatePeople as it is creating people.
        {
            for (int j = 0; j < i; j++)
            {
                try
                {
                    // Creates a dandon Name   // ❌ Typo : It should "Randon" not "dandon" because we are using Randon function to assing name.
                    string name = string.Empty;   // ❌ // Use predefined array (called "names") & it should be outside of loop & then use "string name = names[random.Next(0, names.Length)];" inside loop to get a randon name.
                    var random = new Random();   // ❌ Declare outside of loop

                    // ❌ Issue: 'Next(0, 1)' will always return 0; the range upper bound is exclusive. So "Betty" name will never be assigned to name.
                    if (random.Next(0, 1) == 0)
                    {
                        name = "Bob";
                    }
                    else
                    {
                        name = "Betty";
                    }
                    // Adds new people to the list
                    _people.Add(new People(name, DateTime.UtcNow.Subtract(new TimeSpan(random.Next(18, 85) * 356, 0, 0, 0))));  // ❌ 356?: It should be 365.
                }
                catch (Exception e)
                {
                    // Dont think this should ever happen
                    throw new Exception("Something failed in user creation");   // ❌ Generic exception : Use 'throw;' or pass original exception as inner exception.
                }
            }
            return _people;
        }

        private IEnumerable<People> GetBobs(bool olderThan30)
        {
            // ❌ 356?: It should be 365.
            return olderThan30 ? _people.Where(x => x.Name == "Bob" && x.DOB >= DateTime.Now.Subtract(new TimeSpan(30 * 356, 0, 0, 0))) : _people.Where(x => x.Name == "Bob");
        }

        // ❌ Add method summary
        public string GetMarried(People p, string lastName)   // ❌ Change parameter naming from "p" to "people"
        {
            // ❌ validation missing : Add below exception
            // Exception 1 : Null exception for people.
            // Exception 2 : Last name cannot be null or empty.

            if (lastName.Contains("test"))   // ❌ Add StringComparison.OrdinalIgnoreCase to ignore case
                return p.Name;
            if ((p.Name.Length + lastName).Length > 255)
            {
                // ❌ Dead code: Substring result is unused. Update the return value.
                (p.Name + " " + lastName).Substring(0, 255);
            }

            return p.Name + " " + lastName;
        }
    }
}