using Lab3_ORM_EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_ORM_EF
{
    internal class UserInterface
    {
        private Dictionary<ConsoleKey, Action> mainMenuActions;
        private ReadTable rt; 
        private ResultSorter rs; 

        public UserInterface(ReadTable reaTab, ResultSorter resSor)
        {
            rt = reaTab;
            rs = resSor;
            mainMenuActions = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.D1, () =>  ReadAllOrSpecificEmployees()}, // Next See all Emps or in one specific category // Read Emps(s)
                {ConsoleKey.D2, () =>  ReadOrderedStuds()}, // Sorting switch ASC/DESC and Last/First Name// Read Students
                {ConsoleKey.D3, () =>  ReadStudsInClass()}, // Show class names // User chooses 1/5 classes // Save choice // Sort ASC/DESC and Last/First Name // Read students in class
                {ConsoleKey.D4, () =>  ReadGradesInSpecificMonth()}, // Read All months where a grade was graded // User chooses Month // Save choice // Read grades in month.
                {ConsoleKey.D5, () =>  ReadCourseGradeStats()}, // Read the CourseGradeStats View.
                {ConsoleKey.D6, () =>  InsertNewStudent()}, // Insert new student into Students
                {ConsoleKey.D7, () =>  InsertNewEmployee()}, // Insert new employee into Employees
                {ConsoleKey.D0, () =>  Environment.Exit(0)}
            };
        }
        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(
            "[1] See Employees\n" +
            "[2] See Students\n" + 
            "[3] See Students in a specified class\n" +
            "[4] See all grades during a specified month\n" +
            "[5] See average, min and max grade for all courses\n" +
            "[6] Add a new Student\n" +
            "[7] Add a new Employee\n" +
            "[0] Exit\n");
        }
        public void MainMenu()
        {

            while (true)
            {
                DisplayMenu();
                var key = Console.ReadKey().Key;

                if (mainMenuActions.TryGetValue(key, out var action))
                {
                    action.Invoke();
                }
                else
                {
                    Console.WriteLine("Invalid option, Please try again.");
                }
            }
        }

        private async void ReadAllOrSpecificEmployees() // Done
        {
            rt.ReadOnlyWorkRole();
            string? workRole = await rs.WorkRoleChoice();

            if (workRole != "Main Menu")
                rt.ReadEmployees(workRole);

            MainMenu();
        }
        private async void ReadOrderedStuds() // Done
        {
            var orderBy = await rs.OrderByWhat();
            rt.ReadStudents(orderBy);
            MainMenu();
        }
        private async void ReadStudsInClass() // Done
        {
            rt.ReadOnlyClassNames();
            int classChoice = await rs.ClassChoice();

            
            if (classChoice != 10)
            {
                var orderBy = await rs.OrderByWhat();
                rt.ReadStudentsInClass(classChoice, orderBy);
            }
            MainMenu();
        }
        private async void ReadGradesInSpecificMonth() // Done
        {
            rt.ReadOnlyGradedMonths(); 

            int chosenMonth = await rs.MonthChoice(); 

            if (chosenMonth != 10)
                rt.ReadGradesInSpecificMonth(chosenMonth);
            
            MainMenu();
        }
        private void ReadCourseGradeStats() // Done
        {
            rt.ReadCourseGradeStats();
            MainMenu();
        }
        private void InsertNewStudent() // TODO - TEST 
        {
            rt.InsertNewStudent();
            MainMenu();
        }
        private void InsertNewEmployee() // TODO - TEST
        {
            rt.InsertNewEmployee();
            MainMenu();
        }

    }
}
