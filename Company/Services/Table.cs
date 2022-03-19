using System;
using System.IO;
using System.Linq;

namespace Company
{
	class Table
	{
		delegate void OutputMessage(string str);

		private string tableHeader;
		private string[] columnHeaders;
		private int[] columnWidth;
		string fileName;
		OutputMessage Message;

		private void OutputToTheConsole(string str)
		{
			Console.Write(str);
		}

		private void OutputToTheFile(string str)
		{
			using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
			{
				sw.Write(str);
			}
		}

		public static void ClearFile(string fileName)
		{
			using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Default))
			{

			}
		}

		public Table(string tableHeader, string[] columnHeaders, int[] columnWidth, string fileName = "")
		{
			this.tableHeader = tableHeader;
			this.columnHeaders = columnHeaders;
			this.columnWidth = columnWidth;
			this.fileName = fileName;

			if (columnHeaders.Length != columnWidth.Length)
				throw new Exception("Количество элемнтов в массивах не совпадает");

			for (int i = 0; i < columnHeaders.Length; i++)
			{
				if (columnHeaders[i].Length > columnWidth[i])
					throw new Exception("Ширина названия столбца больше ширины столбца");
			}

			if (fileName == "")
			{
				Message = OutputToTheConsole;
			}
			else
			{
				try
				{
					using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
					{
						Message = OutputToTheFile;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		public void Hat()
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Message(tableHeader + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Message("╔");
			int count = 0;

			foreach (var i in columnWidth)
			{
				Message(string.Join("", Enumerable.Repeat('═', i)));
				if (count != columnWidth.Length - 1)
				{
					Message("╦");
				}
				else
				{
					Message("╗\n");
				}
				count++;
			}

			Message("║");
			int index = 0;

			foreach (var i in columnWidth)
			{
				int quantity = columnHeaders[index].Length;
				Message(string.Join("", Enumerable.Repeat(' ', i - quantity)));
				Message($"{columnHeaders[index]}║");
				index++;
			}

			Message("\n");
			Message("╠");
			count = 0;

			foreach (var i in columnWidth)
			{
				Message(string.Join("", Enumerable.Repeat('═', i)));
				if (count != columnWidth.Length - 1)
				{
					Message("╬");
				}
				else
				{
					Message("╣\n");
				}
				count++;
			}
		}

		/// <summary>
		/// Для вывода bool, DateTime и некоторых других типов лучше использовать .ToString()
		/// </summary>
		/// <param name="data"></param>
		public void Body(object[] data)
		{
			Message("║");
			int index = 0;
			int quantity;

			foreach (var i in columnWidth)
			{
				if (data[index] is double @double)
				{
					int number;
					if (@double >= 0)
					{
						number = (int)Math.Ceiling(@double);
					}
					else
					{
						number = (int)Math.Floor(@double);
					}
					quantity = number.ToString().Length - 2;
				}
				else if (data[index] is string @string)
				{
					quantity = @string.Length;
				}
				else if (data[index] is int @int)
				{
					quantity = @int.ToString().Length;
				}
				else
				{
					quantity = 0;
				}

				Message(string.Join("", Enumerable.Repeat(' ', i - quantity)));

				if (data[index] is int)
				{
					Message($"{data[index]}║");
				}
				else
				{
					Message($"{data[index]:f2}║");
				}

				index++;
			}

			Message("\n");
		}

		public void Bottom()
		{
			Message("╚");
			int count = 0;

			foreach (var i in columnWidth)
			{
				Message(string.Join("", Enumerable.Repeat('═', i)));

				if (count != columnWidth.Length - 1)
				{
					Message("╩");
				}
				else
				{
					Message("╝\n");
				}

				count++;
			}
		}
	}
}