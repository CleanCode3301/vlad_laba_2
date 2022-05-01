using System.IO;
using System.Threading.Tasks;
using Company.Models;

namespace Company.Services
{
	internal class TextFile
	{
		private string _fileName;

		public TextFile()
		{
			_fileName = "Employee.txt";
		}

		public async Task WriterAsync(EmployeCollection<Employee> collection)
		{
			using (StreamWriter writer = new StreamWriter(_fileName, false))
			{
				foreach (Employee item in collection)
				{
					await writer.WriteLineAsync(item.ToString());
				}
			}
		}

		public async Task<EmployeCollection<Employee>> ReaderAsync()
		{
			var collection = new EmployeCollection<Employee>();

			using (StreamReader reader = new StreamReader(_fileName))
			{
				string? line;

				while ((line = await reader.ReadLineAsync()) != null)
				{
					string[] words = line.Split(";");

					if (words.Length > 3)
					{
						collection.Add(new Teacher(words[0], int.Parse(words[1]), words[2], int.Parse(words[3]), int.Parse(words[4]), int.Parse(words[5])));
					}
					else
					{
						collection.Add(new Employee(words[0], int.Parse(words[1]), words[2]));
					}
				}
			}

			return collection;
		}
	}
}
