namespace Company.Models
{
	public class Teacher : Employee
	{
		public int Salary { get; set; }
		public int AdditionalPaymentForExperience { get; set; }
		public int Prize { get; set; }

		public Teacher(string lastName, int year, string post, int salary, int additionalPaymentForExperience, int prize) : base(lastName, year, post)
		{
			Salary = salary;
			AdditionalPaymentForExperience = additionalPaymentForExperience;
			Prize = prize;
		}
	}
}
