using first_l_s2.DI;
using first_l_s2.Enums;
using first_l_s2.Models;

namespace first_l_s2.DAO
{
    internal class EmployeeCollection : IData<IEmployee>, IRead
    {
        private readonly List<IEmployee> _employees;
        private readonly string _path;

        public EmployeeCollection(string path) : this(path, new List<IEmployee>()) { }

        public EmployeeCollection(string path, IList<IEmployee> employees)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), "The parameter cannot be null.");
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException(nameof(path), "The file not found.");
            }

            if (employees is null)
            {
                throw new ArgumentNullException(nameof(employees), "The parameter cannot be null.");
            }

            _employees = employees.ToList();
            _path = path;
        }

        public void Add(IEmployee data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "The parameter cannot be null.");
            }

            _employees.Add(data);
        }

        public IEnumerable<IEmployee> ReadAll() => _employees;

        public void Remove(int index)
        {
            if (index < 0 || index >= _employees.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Incorrect index.");
            }

            _employees.RemoveAt(index);
        }

        public void Restore()
        {
            using (StreamReader streamReader = new StreamReader(_path))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] text = line.Split(",");

                    _employees.Add(new Employee(int.Parse(text[0]), text[1],
                        (Department)int.Parse(text[3]), (Position)int.Parse(text[3])));
                }
            }
        }
    }
}
