using Company.Models;
using Company.Services;
using System;
using System.Linq;

namespace Company
{
	internal class ProgramService
	{
		public static void Test()
		{
			var file = new TextFile();

			EmployeCollection<Employee> collection = file.ReaderAsync().Result;

			var table = new Table("Сотрудники", new string[] { "Фамилия", "Год рождения", "Должность" }, new int[] { 10, 20, 20 });

			table.Hat();

			foreach (Employee employee in collection)
			{
				table.Body(new object[] { employee.LastName, employee.Year, employee.Post });
			}

			table.Bottom();

			Console.WriteLine("Младше 30");

			int salary = 50;

			Func<Employee, bool> func = emp =>
			{
				if (emp is Teacher teacher)
				{
					return teacher.Year > DateTime.Now.Year - 30 && teacher.Salary > salary;
				}
				return false;
			};

			collection.Search(func)
				.ForEach(x => Console.WriteLine(x));

			Console.WriteLine("Бухгалтеры старше 50");
			(from item in collection.Queue
			 where item.Year < DateTime.Now.Year - 50 && item.Post == "бухгалтер"
			 select item)
			 .ToList()
			 .ForEach(x => Console.WriteLine(x));

			Console.WriteLine("Средний возраст");
			collection.Queue.GroupBy(x => x.Post)
				.ToList()
				.ForEach(x =>
				Console.WriteLine(x.Key + " " + (DateTime.Now.Year - Convert.ToInt32(x.Average(emp => emp.Year))))
				);
		}
	}
}
