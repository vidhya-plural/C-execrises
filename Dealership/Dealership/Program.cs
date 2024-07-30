using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Dealership.Data;
using Dealership.Services;

namespace Dealership
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DealershipContext>(options =>
                    options.UseSqlServer("Server=localhost;Database=Dealership;User Id=sa;Password=Password@123;TrustServerCertificate=True;"))
                .AddScoped<CarService>()
                .AddScoped<SaleService>()
                .BuildServiceProvider();

            var carService = serviceProvider.GetService<CarService>();
            var saleService = serviceProvider.GetService<SaleService>();

            while (true)
            {
                Console.WriteLine("1 - List all available cars");
                Console.WriteLine("2 - List available cars with less than a specific odometer reading");
                Console.WriteLine("3 - List available cars with a specific make and model");
                Console.WriteLine("4 - List available cars between a specific price range");
                Console.WriteLine("5 - Sell a car");
                Console.WriteLine("6 - Change a car’s price");
                Console.WriteLine("7 - Delete a car from inventory");
                Console.WriteLine("8 - Quit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        carService.ListAvailableCars();
                        break;
                    case "2":
                        Console.Write("Enter the maximum odometer reading: ");
                        int maxOdometer = Convert.ToInt32(Console.ReadLine());
                        carService.ListCarsByOdometer(maxOdometer);
                        break;
                    case "3":
                        Console.Write("Enter the make: ");
                        string make = Console.ReadLine();
                        Console.Write("Enter the model: ");
                        string model = Console.ReadLine();
                        carService.ListCarsByMakeAndModel(make, model);
                        break;
                    case "4":
                        Console.Write("Enter the minimum price: ");
                        decimal minPrice = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter the maximum price: ");
                        decimal maxPrice = Convert.ToDecimal(Console.ReadLine());
                        carService.ListCarsByPriceRange(minPrice, maxPrice);
                        break;
                    case "5":
                        Console.Write("Enter the inventory number of the car being sold: ");
                        string inventoryNumber = Console.ReadLine();
                        saleService.SellCar(inventoryNumber);
                        break;
                    case "6":
                        Console.Write("Enter the inventory number of the car: ");
                        string invNumberForPriceChange = Console.ReadLine();
                        Console.Write("Enter the new price: ");
                        decimal newPrice = Convert.ToDecimal(Console.ReadLine());
                        carService.ChangeCarPrice(invNumberForPriceChange, newPrice);
                        break;
                    case "7":
                        Console.Write("Enter the inventory number of the car to delete: ");
                        string invNumberToDelete = Console.ReadLine();
                        carService.DeleteCar(invNumberToDelete);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
