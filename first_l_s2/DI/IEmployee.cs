using first_l_s2.Enums;

namespace first_l_s2.DI
{
    internal interface IEmployee
    {
        int Age { get; }
        string Surname { get; }
        Department Department { get; }
        Position Position { get; }
    }
}
