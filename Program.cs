using Lab3_ORM_EF.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Lab3_ORM_EF
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            
            var serviceProvider = new ServiceCollection()
                .AddDbContext<Lab3_OrmContext>()
                .AddScoped<ReadTable>() // Register the ReadTable class as scoped
                .AddSingleton<ResultSorter>() // Singleton: no DbContext dependency
                .AddSingleton<UserInterface>() // Singleton: no DbContext dependency
                .BuildServiceProvider();

            var ui = serviceProvider.GetService<UserInterface>(); // Use the injected UserInterface
            ui?.MainMenu(); // Run the menu
        }
    }
}
