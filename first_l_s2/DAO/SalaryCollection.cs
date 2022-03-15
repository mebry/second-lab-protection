using first_l_s2.DI;
using first_l_s2.Enums;
using first_l_s2.Models;

namespace first_l_s2.DAO
{
    internal class SalaryCollection : IData<ISalary>, IRead
    {
        private readonly List<ISalary> _salaries;
        private readonly string _path;

        public SalaryCollection(string path) : this(path, new List<ISalary>()) { }

        public SalaryCollection(string path, IList<ISalary> salaries)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path), "The parameter cannot be null.");
            }

            if (!File.Exists(path))
            {
                throw new ArgumentException(nameof(path), "The file not found.");
            }

            if (salaries is null)
            {
                throw new ArgumentNullException(nameof(salaries), "The parameter cannot be null.");
            }

            _salaries = salaries.ToList();
            _path = path;
        }

        public void Add(ISalary data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "The parameter cannot be null.");
            }

            _salaries.Add(data);
        }

        public IEnumerable<ISalary> ReadAll() => _salaries;

        public void Remove(int index)
        {
            if (index < 0 || index >= _salaries.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Incorrect index.");
            }

            _salaries.RemoveAt(index);
        }

        public void Restore()
        {
            using (StreamReader streamReader = new StreamReader(_path))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] text = line.Split(",");

                    _salaries.Add(new Wages(int.Parse(text[0]), (Position)int.Parse(text[1])));
                }
            }
        }
    }
}
