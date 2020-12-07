using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLinqLib
{
	public class Repository
	{
		private List<EmployeeInfo> _employees;
		private List<DepartmentInfo> _departments;

		public Repository()
		{
			_employees = new List<EmployeeInfo>();
			_departments = new List<DepartmentInfo>();

			InitializeDepartments();
			InitializeEmployeers();

		}

		private void InitializeDepartments()
		{
			_departments.Add(new DepartmentInfo() { Id = 1, Name = "", DateCreated = new DateTime(2010, 01, 01) });
			_departments.Add(new DepartmentInfo() { Id = 2, Name = "Marketing", DateCreated = new DateTime(2010,02,10)});
			_departments.Add(new DepartmentInfo() { Id = 3, Name = "Operations", DateCreated = new DateTime(2010, 02, 15) });
			_departments.Add(new DepartmentInfo() { Id = 4, Name = "Production", DateCreated = new DateTime(2010, 03, 05) });
			_departments.Add(new DepartmentInfo() { Id = 5, Name = "Finance", DateCreated = new DateTime(2010, 01, 10) });
		}

		private void InitializeEmployeers()
		{
			_employees.Add(new EmployeeInfo() { Id = 1, ParentId = 0, Name = "Gregory S. Price", DepartmentId = 1, Position = "President", Salary = 10000});
			_employees.Add(new EmployeeInfo() { Id = 2, ParentId = 1, Name = "Irma R. Marshall", DepartmentId = 2, Position = "Vice President", Salary = 8000 });
			_employees.Add(new EmployeeInfo() { Id = 3, ParentId = 1, Name = "John C. Powell", DepartmentId = 3, Position = "Vice President", Salary = 7000 });
			_employees.Add(new EmployeeInfo() { Id = 4, ParentId = 1, Name = "Christian P. Laclair", DepartmentId = 4, Position = "Vice President", Salary = 7500 });
			_employees.Add(new EmployeeInfo() { Id = 5, ParentId = 1, Name = "Karen J. Kelly", DepartmentId = 5, Position = "Vice President", Salary = 5000 });
			_employees.Add(new EmployeeInfo() { Id = 6, ParentId = 2, Name = "Brian C. Cowling", DepartmentId = 2, Position = "Manager", Salary = 2500 });
			_employees.Add(new EmployeeInfo() { Id = 7, ParentId = 2, Name = "Thomas C. Dawson", DepartmentId = 2, Position = "Manager", Salary = 3500 });
			_employees.Add(new EmployeeInfo() { Id = 8, ParentId = 2, Name = "Angel M. Wilson", DepartmentId = 2, Position = "Manager", Salary = 2700 });
			_employees.Add(new EmployeeInfo() { Id = 9, ParentId = 2, Name = "Bryan R. Henderson", DepartmentId = 2, Position = "Manager", Salary = 3000 });
			_employees.Add(new EmployeeInfo() { Id = 10, ParentId = 3, Name = "Harold S. Brandes", DepartmentId = 3, Position = "Manager", Salary = 2900 });
			_employees.Add(new EmployeeInfo() { Id = 11, ParentId = 3, Name = "Michael S. Blevins", DepartmentId = 3, Position = "Manager", Salary = 2450 });
			_employees.Add(new EmployeeInfo() { Id = 12, ParentId = 3, Name = "Jan K. Sisk", DepartmentId = 3, Position = "Manager", Salary = 2700 });
			_employees.Add(new EmployeeInfo() { Id = 13, ParentId = 3, Name = "Sidney L. Holder", DepartmentId = 3, Position = "Manager", Salary = 2800 });
			_employees.Add(new EmployeeInfo() { Id = 14, ParentId = 4, Name = "James L. Kelsey", DepartmentId = 4, Position = "Manager", Salary = 3000 });
			_employees.Add(new EmployeeInfo() { Id = 15, ParentId = 4, Name = "Howard M. Carpenter", DepartmentId = 4, Position = "Manager", Salary = 2700 });
			_employees.Add(new EmployeeInfo() { Id = 16, ParentId = 4, Name = "Jennifer T. Tapia", DepartmentId = 4, Position = "Manager", Salary = 2800 });
			_employees.Add(new EmployeeInfo() { Id = 17, ParentId = 5, Name = "Judith P. Underhill", DepartmentId = 5, Position = "Manager", Salary = 2500 });
			_employees.Add(new EmployeeInfo() { Id = 18, ParentId = 5, Name = "Russell E. Belton", DepartmentId = 5, Position = "Manager", Salary = 2600 });	
		}

		public List<EmployeeInfo> GetEmployees()
		{
			return _employees;
		}

		public List<DepartmentInfo> GetDepartments()
		{
			return _departments;
		}
	}
}
