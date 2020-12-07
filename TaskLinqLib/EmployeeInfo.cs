using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLinqLib
{
	public class EmployeeInfo
	{
		public int Id { get; set; }
		public int ParentId { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public int DepartmentId { get; set; }
		public decimal Salary { get; set;  }
	}
}
