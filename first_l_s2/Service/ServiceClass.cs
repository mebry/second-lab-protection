using first_l_s2.DAO;
using first_l_s2.DI;
using first_l_s2.Enums;
using ShowTable;

namespace first_l_s2
{
    public class ServiceClass
    {
        private static string _employeePath = "employees.txt";
        private static string _salaryPath = "salaries.txt";

        public static void MainProcess()
        {
            IData<IEmployee> employees = new EmployeeCollection(_employeePath);

            if (employees is IRead employeesRead)
            {
                employeesRead.Restore();
            }

            IData<ISalary> salaries = new SalaryCollection(_salaryPath);

            if (salaries is IRead salariesRead)
            {
                salariesRead.Restore();
            }

            ShowEmployees(employees.ReadAll(), "Employees");
            ShowSalaries(salaries.ReadAll(), "Salaries");

            var sortEmployeeLessThanThirthyQuery = from employee in employees.ReadAll()
                                                   where employee.Age < 30
                                                   orderby employee.Surname
                                                   select employee;

            ShowEmployees(sortEmployeeLessThanThirthyQuery, "Sorted Employees");

            var findDepertmentsWithoutRepetitionQuery = employees.ReadAll().Select(i => i.Department).Distinct();

            Console.WriteLine("Departments without repetition: " +
                string.Join(", ", findDepertmentsWithoutRepetitionQuery));

            Console.WriteLine();

            var findEmployeeWithAverageAgeQuery = employees.ReadAll().
                GroupBy(i => i.Department).OrderByDescending(i => i.Average(j => j.Age));

            foreach (var item in findEmployeeWithAverageAgeQuery)
            {
                Console.WriteLine($"Department: {item.Key}\n\tAverage age: {item.Average(i => i.Age)}");
            }

            Console.WriteLine("1)Marketing\n" +
                "2)Programming\n" +
                "3)Management");

            int departmentNumber = ConsoleInput.ConsoleInputData.InputDataFromConsole<int>
                ("Input number department:", 1, 1, 1, 3) - 1;

            var findEmployeesQuery = from employee in employees.ReadAll()
                                     join salary in salaries.ReadAll()
                                     on employee.Position equals salary.Position
                                     where employee.Department == (Department)departmentNumber
                                     select new { Surname = employee.Surname, Position = salary.Position, Salary = salary.Salary };

            Console.WriteLine((Department)departmentNumber);

            foreach (var item in findEmployeesQuery)
            {
                Console.WriteLine($"Surname: {item.Surname}, position: {item.Position}, salary:{item.Salary}");
            }

            var findDepartmentQuery = from employee in employees.ReadAll()
                                  join salary in salaries.ReadAll()
                                  on employee.Position equals salary.Position
                                  let t = new { Department = employee.Department, Salary = salary.Salary }
                                  group t by t.Department into g
                                  select new { Department = g.Key, Salary = g.Average(i => i.Salary) };

            var department = findDepartmentQuery.First(i => i.Salary == findDepartmentQuery.Max(j => j.Salary));

            Console.WriteLine("\ndepartment:"+ department.Department+" average salary:"+ department.Salary+"\n");

            var CountOfStudentsQuery = from employee in employees.ReadAll()
                                 group employee by employee.Department into g
                                 select new { Department = g.Key, Count = g.Count() };

            foreach (var item in CountOfStudentsQuery)
            {
                Console.WriteLine($"department: {item.Department}, count: {item.Count}");
            }

            var findAgeQuery=from employee in employees.ReadAll()
                             group employee by employee.Position into g
                             select new { Position = g.Key, Age = g.Min(i => i.Age) };

            foreach (var item in findAgeQuery)
            {
                Console.WriteLine($"Position: {item.Position}, min age: {item.Age}");
            }
        }

        private static void ShowEmployees(IEnumerable<IEmployee> employees, string tableName)
        {
            string[] colomns = { "Age", "Surname", "Department", "Position" };

            int[] lengthColomns = TableWithColumns.CalculatingTheSizeByHeaders(colomns, 5);

            TableWithColumns tableWithColumns = new TableWithColumns(colomns, lengthColomns, tableName);

            tableWithColumns.ChangingTheEntireColorTheme(ConsoleColor.DarkRed, ConsoleColor.DarkYellow,
                ConsoleColor.DarkBlue, ConsoleColor.Magenta);

            if (employees.Count() > 0)
            {
                tableWithColumns.Head();

                foreach (var employee in employees)
                {
                    tableWithColumns.Body(new object[]
                        { employee.Age, employee.Surname, employee.Department, employee.Position });
                }

                tableWithColumns.Bottom();

                Console.WriteLine();
            }
        }

        private static void ShowSalaries(IEnumerable<ISalary> salaries, string tableName)
        {
            string[] colomns = { "Salary", "Position" };

            int[] lengthColomns = TableWithColumns.CalculatingTheSizeByHeaders(colomns, 5);

            TableWithColumns tableWithColumns = new TableWithColumns(colomns, lengthColomns, tableName);

            tableWithColumns.ChangingTheEntireColorTheme(ConsoleColor.DarkRed, ConsoleColor.DarkYellow,
                ConsoleColor.DarkBlue, ConsoleColor.Magenta);

            if (salaries.Count() != 0)
            {
                tableWithColumns.Head();

                foreach (var item in salaries)
                {
                    tableWithColumns.Body(new object[] { item.Salary, item.Position });
                }

                tableWithColumns.Bottom();
            }
        }
    }
}