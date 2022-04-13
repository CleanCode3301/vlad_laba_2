using Company.Models;
using Company.Services;
using System;
using System.Collections.Generic;
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

			Func<Employee, bool> func = emp => emp.Year < DateTime.Now.Year - 30;

			List<Employee> list = collection.Search(func);

			var listAccountent = (from item in collection.Queue
								  where item.Year > DateTime.Now.Year - 50 && item.Post == "Бухгалтер"
								  select item).ToList();
			var listAverage = collection.Queue.GroupBy(x => x is Teacher).Average(x =>
			{
				if (x is Teacher teacher)
					return teacher.Salary;
				return 0;
			});
		}
	}
}
