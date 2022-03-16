using first_l_s2.DI;
using first_l_s2.Enums;

namespace first_l_s2.Models
{
    internal class Wages : ISalary
    {
        public Wages(decimal salary, Position position)
        {
            if (salary < 0)
            {
                throw new ArgumentException(nameof(salary), "Incorrect parameter.");
            }

            Salary = salary;
            Position = position;
        }

        public decimal Salary { get; }

        public Position Position { get; }
    }
}
