using first_l_s2.Enums;

namespace first_l_s2.DI
{
    internal interface ISalary
    {
        decimal Salary { get; }
        Position Position { get; }
    }
}
