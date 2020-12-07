using System;
using System.Collections.Generic;
using TaskLinqLib;
using System.Linq;

namespace TaskLinqApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            var employees = repository.GetEmployees();
            var departments = repository.GetDepartments();

            // 1. Вывести зарплату только менеджеров
            // Вывод - Name, Position, Salary

            Console.WriteLine("----- Task 1 -----");

            var list = from value in employees
                       where value.ParentId > 1
                       select value;

            foreach (var item in list)
            {
                Console.WriteLine($"{item.Name} - {item.Position} - {item.Salary}");
            }

            Console.WriteLine();

            var list1 = employees.Where(emp => emp.ParentId > 1);

            foreach (var item in list1)
            {
                Console.WriteLine($"{item.Name} - {item.Position} - {item.Salary}");
            }

            Console.WriteLine();

            // 2. Вывести среднюю зарплату по должностям. Сортировка по зарплате
            // Вывод - Position, Salary

            Console.WriteLine("----- Task 2 -----");

            var list2 = from value in employees
                        orderby value.Salary
                        group value by value.Position into part
                        select
                        new
                        {
                            Position = part.Key,
                            Salary = part.Average(s => s.Salary)
                        };

            foreach (var item in list2)
            {
                Console.WriteLine($"{item.Position}. Средняя зп: {Math.Round(item.Salary, 2)}");
            }

            Console.WriteLine();

            var list3 = employees.
                                OrderBy(p => p.Salary).
                                GroupBy(p => p.Position).
                                Select(gr => new { Position = gr.Key, Salary = gr.Average(s => s.Salary) });

            foreach (var item in list3)
            {
                Console.WriteLine($"{item.Position}. Средняя зп: {Math.Round(item.Salary, 2)}");
            }

            Console.WriteLine();

            // 3. Вывести информацию о сотруднике с выводом Name его начальника
            // Вывод - Name, Position, ChieName

            Console.WriteLine("----- Task 3 -----");

            var list4 = from value in employees
                        where value.ParentId > 1
                        select new
                        {
                            Name = value.Name,
                            Position = value.Position,
                            ChiefName = (from chief in employees
                                         where chief.DepartmentId == value.DepartmentId && chief.ParentId == 1
                                         select chief.Name).First()
                        };


            foreach (var item in list4)
            {
                Console.WriteLine($"{item.Name} - {item.Position} - Chief: {item.ChiefName}");
            }

            Console.WriteLine();

            var list5 = employees.
                                Where(value => value.ParentId > 1).
                                Select(gr => new
                                {
                                    Name = gr.Name,
                                    Position = gr.Position,
                                    ChiefName = (employees.Where(emp => emp.DepartmentId == gr.DepartmentId && emp.ParentId == 1).
                                    Select(prt => new { prt.Name }).First())
                                });

            foreach (var item in list5)
            {
                Console.WriteLine($"{item.Name} - {item.Position} - Chief: {item.ChiefName.Name}");
            }

            Console.WriteLine();

            // 4. Вывести информацию об отделах  с выводом информации  - кол-во сотрудников в данном отделе, суммарной ЗП отдела
            // Вывод - DepartmentName, CountEmployees, TotalSalary

            Console.WriteLine("----- Task 4 -----");

            var list6 = from deport in departments
                        join empl in employees
                        on deport.Id equals empl.DepartmentId
                        group empl by empl.DepartmentId into part
                        select new
                        {
                            CountEmployees = part.Count(),
                            TotalSalary = part.Sum(s => s.Salary),
                            DepartmentName = (from deport in departments
                                              where deport.Id == part.Key
                                              select deport.Name).First()
                        };


            foreach (var item in list6)
            {
                Console.WriteLine($"Department: {item.DepartmentName} - Quantity employee: {item.CountEmployees} - Sum ZP: {item.TotalSalary}");
            }

            Console.WriteLine();

            var list7 = departments.Join(employees, deport => deport.Id, empl => empl.DepartmentId,
                        (deport, empl) => new { Deport = deport, Empl = empl }).
                        GroupBy(p => p.Empl.DepartmentId).
                        Select(gr => new
                        {
                            CountEmployees = gr.Count(),
                            TotalSalary = gr.Sum(s => s.Empl.Salary),
                            DepartmentName = (departments.Where(emp => emp.Id == gr.Key).Select(prt => new { prt.Name }).First())
                        });


            foreach (var item in list7)
            {
                Console.WriteLine($"Department: {item.DepartmentName.Name} - Quantity employee: {item.CountEmployees} - Sum ZP: {item.TotalSalary}");
            }

            Console.WriteLine();

            // 5. Вывести информацию о сотрудниках отдела, который был создан самым  последним
            // Вывод - DepartmentName, DateCreated, Список сотрудников (Name, Position)

            Console.WriteLine("----- Task 5 -----");

            var list8 = from deport in departments
                        join empl in employees
                        on deport.Id equals empl.DepartmentId
                        where deport.DateCreated == departments.Max(max => max.DateCreated)
                        select new
                        {
                            DepartmentName = deport.Name,
                            DateCreated = deport.DateCreated,
                            Name = empl.Name,
                            Position = empl.Position
                        };

            Console.WriteLine($"Department: {list8.Select(p => p.DepartmentName).First()}");
            Console.WriteLine($"Date created: {list8.Select(p => p.DateCreated).First()}");


            foreach (var item in list8)
            {
                Console.WriteLine($"Name: {item.Name} - Position: {item.Position}");
            }

            Console.WriteLine();

            var list9 = departments.Join(employees, deport => deport.Id, empl => empl.DepartmentId,
                        (deport, empl) => new { Deport = deport, Empl = empl }).
                        Where(value => value.Deport.DateCreated == departments.Max(max => max.DateCreated)).
                        Select(gr => new
                        {
                            DepartmentName = gr.Deport.Name,
                            DateCreated = gr.Deport.DateCreated,
                            Name = gr.Empl.Name,
                            Position = gr.Empl.Position

                        });

            Console.WriteLine($"Department: {list8.Select(p => p.DepartmentName).First()}");
            Console.WriteLine($"Date created: {list8.Select(p => p.DateCreated).First()}");


            foreach (var item in list9)
            {
                Console.WriteLine($"Name: {item.Name} - Position: {item.Position}");
            }

            Console.WriteLine();

            // 6. Вевести информацию о должностях отделов, которые были созданы в марте и феврале 2010 года
            // Сортировка по наименованию отдела, дате создания, должности
            // Вывод - DepartmentName,DateCreated, Список должностей

            Console.WriteLine("----- Task 6 -----");

            var list10 = from deport in departments
                         where deport.DateCreated.Month == 3 || deport.DateCreated.Month == 2
                         orderby deport.Name, deport.DateCreated
                         group deport by new { deport.Name, deport.DateCreated }
                         into gr
                         select new
                         {
                             DepartmentName = gr.Key.Name,
                             DateCreated = gr.Key.DateCreated,
                             Position = (from deport in departments
                                         join empl in employees
                                         on deport.Id equals empl.DepartmentId
                                         where (deport.DateCreated.Month == 3 || deport.DateCreated.Month == 2)
                                         orderby empl.Position
                                         group empl by empl.Position into part
                                         select part.Key)
                         };


            foreach (var item in list10)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                Console.WriteLine($"Date: {item.DateCreated.ToShortDateString()}");

                foreach (var item1 in item.Position)
                {
                    Console.WriteLine($"Position: {item1}");
                }
                Console.WriteLine("--------------------------");
            }

            Console.WriteLine();

            var list11 = departments.Where(emp => emp.DateCreated.Month == 3 || emp.DateCreated.Month == 2).
                        OrderBy(ord => ord.Name).ThenBy(ord => ord.DateCreated).
                        GroupBy(gr => new { gr.Name, gr.DateCreated }).
                        Select(sl => new
                        {
                            DepartmentName = sl.Key.Name,
                            DateCreated = sl.Key.DateCreated,
                            Position = (departments.Join(employees, d => d.Id, e => e.DepartmentId,
                            (d, e) => new { deport = d, empl = e }).
                            Where(emp => emp.deport.DateCreated.Month == 3 || emp.deport.DateCreated.Month == 2).
                            OrderBy(ord => ord.empl.Position).
                            GroupBy(gr => gr.empl.Position).
                            Select(pr => new { pr.Key }))
                        });
           

            foreach (var item in list11)
            {
                Console.WriteLine($"Department: {item.DepartmentName}");
                Console.WriteLine($"Date: {item.DateCreated.ToShortDateString()}");

                foreach (var item1 in item.Position)
                {
                    Console.WriteLine($"Position: {item1.Key}");
                }
                Console.WriteLine("--------------------------");
            }

            Console.ReadKey();
        }
    }
}
