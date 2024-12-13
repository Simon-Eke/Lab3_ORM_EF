using Lab3_ORM_EF.Data;
using Lab3_ORM_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab3_ORM_EF
{
    internal class ResultSorter
    {
        
        private readonly Random random = new Random();
        
        public async Task<Func<IQueryable<Person>, IOrderedQueryable<Person>>> OrderByWhat() // parameter possible (Result that is going to be sorted (IF!!!) its about people)
        {

            Console.Clear();
            // What result is going to be sorted?
            // string whom as in "[X] Sort {WHOM???} by ....

            Console.WriteLine("" +
            "[1]\tSort by First Name in ascending order\n" +
            "[2]\tSort by First Name in descending order\n" +
            "[3]\tSort by Last Name in ascending order\n" +
            "[4]\tSort by Last Name in descending order\n");

            Func<IQueryable<Person>, IOrderedQueryable<Person>> orderByChoice;
            ConsoleKey keyChoice = Console.ReadKey().Key;

            while (true)
            {
                switch (keyChoice)
                {
                    case ConsoleKey.D1: // First Name Ascending
                        return orderByChoice = query => query.OrderBy(p => p.FirstName);

                    case ConsoleKey.D2: // First Name Descending
                        return orderByChoice = query => query.OrderByDescending(p => p.FirstName);

                    case ConsoleKey.D3: // Last Name Ascending
                        return orderByChoice = query => query.OrderBy(p => p.LastName);

                    case ConsoleKey.D4: // Last Name Descending
                        return orderByChoice = query => query.OrderByDescending(p => p.LastName);

                    default:            // Default: First Name Ascending
                        Console.WriteLine("Invalid choice. Defaulting to Order by First Name ascending.");
                        return orderByChoice = query => query.OrderBy(p => p.FirstName);
                }
            }
        }

        public async Task<int> ClassChoice()
        {

            ConsoleKey keyChoice = Console.ReadKey().Key; // Valid classes are 50, 51, 52, 53, 54

            return keyChoice switch
            {
                ConsoleKey.D1 => 1,
                ConsoleKey.D2 => 2,
                ConsoleKey.D3 => 3,
                ConsoleKey.D4 => 4,
                ConsoleKey.D5 => 5,
                ConsoleKey.D0 => 10,
                _ => random.Next(1, 6)
            };
        }

        public async Task<string?> WorkRoleChoice()
        {
            ConsoleKey keyChoice = Console.ReadKey().Key;

            return keyChoice switch
            {
                ConsoleKey.D1 => "Administrator",
                ConsoleKey.D2 => "Janitor",
                ConsoleKey.D3 => "Other",
                ConsoleKey.D4 => "Principal",
                ConsoleKey.D5 => "Substitute Teacher",
                ConsoleKey.D6 => "Teacher",
                ConsoleKey.D0 => "Main Menu",
                _ => string.Empty
            };
        }

        public async Task<int> MonthChoice()
        { 

            ConsoleKey keyChoice = Console.ReadKey().Key; // Valid Months are 2, 3, 4, 5, 6

            return keyChoice switch
            {
                ConsoleKey.D2 => 2,
                ConsoleKey.D3 => 3,
                ConsoleKey.D4 => 4,
                ConsoleKey.D5 => 5,
                ConsoleKey.D6 => 6,
                ConsoleKey.D0 => 10, // Return to Main Menu
                _ => random.Next(2, 7)
            };
        }
    }
}
