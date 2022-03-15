using first_l_s2.DI;
using first_l_s2.Enums;

namespace first_l_s2.Models
{
    internal class Employee : IEmployee
    {
        public Employee(int age, string surname, Department department, Position position)
        {
            if(age>110||age<0)
            {
                throw new ArgumentException(nameof(age), "Incorrect parameter.");
            }

            if (surname is null)
            {
                throw new ArgumentNullException(nameof(surname), "The parameter cannot be null.");
            }

            Age = age;
            Surname = surname;
            Department = department;
            Position = position;
        }

        public int Age { get; }

        public string Surname { get; }

        public Department Department { get; }

        public Position Position { get; }
    }
}
