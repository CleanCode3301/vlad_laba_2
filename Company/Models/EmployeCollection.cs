using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Company.Models
{
	public class EmployeCollection<T> : IEnumerable where T : Employee
	{
		private Queue<T> queue;

		public Queue<T> Queue { get { return queue; } }

		public EmployeCollection()
		{
			queue = new Queue<T>();
		}

		public void Add(T emp)
			=> queue.Enqueue(emp);
		public IEnumerator GetEnumerator() => queue.GetEnumerator();

		public List<T> Search(Func<T, bool> emp)
			=> (from i in queue where emp(i) select i).ToList();


	}
}