using Lab3_ORM_EF.Data;
using Lab3_ORM_EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Lab3_ORM_EF
{
    internal class ReadTable
    {
        
        private readonly Lab3_OrmContext context;

        public ReadTable(Lab3_OrmContext DBContext)
        {
            context = DBContext;
        }
        // Option 1
        public void ReadOnlyWorkRole()
        {
            Console.Clear();

            var workRoles = context.WorkRoles
                .Select(wr => wr)
                .Distinct()
                .ToList();

            int counter = 1;

            Console.WriteLine("Choose to see all Employees or the Employees with a specific workrole:\n");
            foreach (var workRole in workRoles)
            {
                Console.WriteLine($"[{counter}]\t{workRole.WorkRole1}");
                counter++;
            }
            Console.WriteLine("[Any]\tDefault to All Employees\n" +
                              "[0]\tReturn to Menu");
        }
        public void ReadEmployees(string? workRole)
        {
            Console.Clear();

            var query = context.Employees
               .Join(context.People,
                    e => e.FkpersonId,
                    p => p.PersonId,
                    (e, p) => new { Employee = e, Person = p });


            if (!string.IsNullOrEmpty(workRole))
                query = query.Where(ep => ep.Employee.FkworkRole == workRole);

            var employees = query
                .Select(ep => new
                { 
                    ep.Person.FirstName,
                    ep.Person.LastName,
                    WorkRole = ep.Employee.FkworkRoleNavigation.WorkRole1
                })
                .ToList();

            string chosenEmployees = !string.IsNullOrEmpty(workRole) ? $"{workRole}(s):\n" : "All Employees:\n";

            Console.WriteLine(chosenEmployees);
            if (employees.Count == 0)
                Console.WriteLine("No Employee exist with that WorkRole.");

            foreach (var employee in employees)
            {
                Console.Write($"Name: {employee.FirstName} {employee.LastName} \tWorkRole: {employee.WorkRole}\n");
            }
            Console.ReadLine();

        }
        // Option 2
        public void ReadStudents(Func<IQueryable<Person>, IOrderedQueryable<Person>> orderByChoice)
        {
            Console.Clear();

            var students = orderByChoice(
                    context.Students
                    .Join(context.People,
                        s => s.PersonId,
                        p => p.PersonId,
                        (s, p) => p)
                    ).ToList();

            Console.WriteLine("Students:\n");
            foreach (var student in students)
            {
                Console.Write($"Name: {student.FirstName} {student.LastName}\n");
            }
            Console.ReadLine();
        }
        // Option 3
        public void ReadOnlyClassNames()
        {
            Console.Clear();

            var classes = context.Classes
                .Select(c => c)
                .ToList();

            int counter = 1;

            Console.WriteLine("Classes:\n");
            foreach (var klass in classes)
            {
                Console.WriteLine($"[{counter}]\t{klass.ClassId}\t{klass.ClassName}");
                counter++;
            }
            Console.WriteLine("[Any]\tRandom Class\n" +
                               "[0]\tReturn to Menu");
        }
        public void ReadStudentsInClass(int classChoice, Func<IQueryable<Person>, IOrderedQueryable<Person>> ordByChoice)
        {

            Console.Clear();

            int classID = 50 + classChoice - 1; // Valid classes are 50, 51, 52, 53, 54

            var students = ordByChoice
                    (context.People
                    .Where(p => p.Student.Class.ClassId == classID))
                    .ToList();

            var className = context.Classes
                .Where(c => c.ClassId == classID)
                .ToList();

            Console.WriteLine($"\nStudents:\n");
            foreach (var student in students)
            {
                Console.Write($"Name: {student.FirstName} {student.LastName}\tClass Name: {className[0].ClassName}\n");
            }
            Console.ReadLine();
        }
        // Option 4
        public void ReadOnlyGradedMonths()
        {
            Console.Clear();

            var monthsToChooseFrom = context.Grades
                .Select(g => new
                {
                    g.GradeDate.Month,  // Extract the month number
                    g.GradeDate.Year    // Extract the year to handle multiple years
                })
                .Distinct()  // Get distinct months
                .OrderBy(m => m.Month)  // Order by month
                .ToList()
                .Select(m => new
                {
                    m.Month,
                    MonthName = new DateTime(m.Year, m.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture) // Month in English
                })
                .ToList();

            
            Console.WriteLine("Choose the Month that you wish to see the grades in:\n");
            foreach (var month in monthsToChooseFrom)
            {
                Console.WriteLine($"[{month.Month}]\t- {month.MonthName}");
            }
            Console.WriteLine("[Any]\t- Default to Random Month\n" +
                              "[0]\t- Return to Menu");
        }
        public void ReadGradesInSpecificMonth(int chosenMonth)
        {

            Console.Clear();
            // Calculate the first day of the previous month and the last day of the chosen month
            // Assuming chosenMonth is a DateTime representing the start of the user's chosen month (e.g., January 1, 2024)    
            DateOnly firstDayOfChosenMonth = new DateOnly(2024, chosenMonth, 1); // First day of the chosen month
            DateOnly lastDayOfChosenMonth = firstDayOfChosenMonth.AddMonths(1).AddDays(-1); // Last day of the chosen month



            var query = context.Grades
                .Where(g => g.GradeDate >= firstDayOfChosenMonth && g.GradeDate <= lastDayOfChosenMonth)  // Date range filter
                .OrderByDescending(g => g.FkgradeLetterNavigation.NumericValue)  // Order by NumericValue descending
                .Select(g => new
                {
                    g.FkgradeLetter,
                    g.GradeDate,
                    g.Fkcourse.CourseName,
                    StudentFirstName = g.Fkstudent.Person.FirstName,
                    StudentLastName = g.Fkstudent.Person.LastName
                })
                .ToList();

            Console.WriteLine("Grades set in specified Month:\n");
            foreach (var item in query)
            {
                Console.WriteLine($"Grade Letter: {item.FkgradeLetter}, " +
                                  $"Grade Date: {item.GradeDate}, " +
                                  $"Course: {item.CourseName}, " +
                                  $"Student: {item.StudentFirstName} {item.StudentLastName}");
            }
            Console.ReadLine();
        }
        // Option 5
        public void ReadCourseGradeStats()
        {
            Console.Clear();

            var sortedCourseGrades = context.CourseGradeStats
                              .OrderByDescending(cgs => cgs.AvgGrade)
                              .ToList();


            Console.WriteLine("See average, worst and best grade for all courses:\n");
            foreach (var grade in sortedCourseGrades)
            {
                Console.WriteLine(
                    $"Id: {grade.CourseId}\tCourse: {grade.CourseName}   \tAvg Grade Score: {grade.AvgGrade}\tWorst Grade: {grade.MinGrade}\tBest Grade: {grade.MaxGrade}");
            }
            Console.ReadLine();
        }
        // Option 6
        public void InsertNewStudent() 
        {
            Console.Clear();
            // Input data for new student
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Social Security Number [YYYYMMDD-XXXX]: ");
            string ssn = Console.ReadLine();
            Console.WriteLine("Enter ClassId [50, 51, 52, 53, 54]: ");
            int classID;
            bool parseSuccess = int.TryParse(Console.ReadLine(), out classID);
            if (!parseSuccess)
                classID = 51;

            // Create new Person
            var newPerson = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                SocialSecurityNumber = ssn
            };

            // Add the new Person to the People DbSet
            context.People.Add(newPerson);
            context.SaveChanges(); // Save changes to get the PersonId (this will populate the PersonId field)

            // Now create the Student object and associate it with the created Person
            var newStudent = new Student
            {
                PersonId = newPerson.PersonId, // Foreign Key to Person table
                ClassId = classID // Example class ID
            };

            // Add the new student to the Students DbSet
            context.Students.Add(newStudent);
            context.SaveChanges(); // Save the changes for the new student

            Console.WriteLine("New Student Added.");
            Console.ReadLine();
        }
        // Option 7
        public void InsertNewEmployee() 
        {
            Console.Clear();
            // Input data for new employee
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Social Security Number: ");
            string ssn = Console.ReadLine();
            Console.Write("Enter Work Role [Administrator, Janitor, Other, Principal, Substitute Teacher, Teacher]: ");
            string workRole = Console.ReadLine(); 

            // Create new Person
            var newPerson = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                SocialSecurityNumber = ssn
            };

            // Add the new Person to the People DbSet
            context.People.Add(newPerson);
            context.SaveChanges(); // Save changes to get the PersonId (this will populate the PersonId field)

            // Now create the Employee object and associate it with the created Person
            var newEmployee = new Employee
            {
                FkpersonId = newPerson.PersonId, // Foreign Key to Person table
                FkworkRole = workRole // Example work role
            };

            // Add the new employee to the Employees DbSet
            context.Employees.Add(newEmployee);
            context.SaveChanges(); // Save the changes for the new employee

            
            Console.WriteLine("New Employee Added");
            Console.ReadLine();

        }
    }
}
