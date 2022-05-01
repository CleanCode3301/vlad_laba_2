using System;

namespace Company.Models
{
	public class Employee : IComparable<Employee>
	{
		public string LastName { get; set; }
		public int Year { get; set; }
		public string Post { get; set; }

		public Employee(string lastName, int year, string post)
		{
			LastName = lastName;
			Year = year;
			Post = post;
		}

		public override bool Equals(object obj)
			=> obj is Employee employee &&
				   LastName == employee.LastName &&
				   Year == employee.Year &&
				   Post == employee.Post;

		public override int GetHashCode() => HashCode.Combine(LastName, Year, Post);

		public override string ToString() => $"{LastName};{Year};{Post}";

		public int CompareTo(Employee other)
			=> string.Compare(LastName, other.LastName);
	}
}
