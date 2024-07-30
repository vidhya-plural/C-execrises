using System;
using System.Linq;
using Dealership.Data;
using Microsoft.EntityFrameworkCore;

namespace Dealership
{
    public class CarService
    {
        private readonly DealershipContext _context;

        public CarService(DealershipContext context)
        {
            _context = context;
        }

        public void ListAvailableCars()
        {
            var cars = _context.Cars.Where(c => c.Status == "available").ToList();
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.InventoryNumber} - {car.Make} {car.Model}, {car.Year}, ${car.Price}");
            }
        }

        public void ListCarsByOdometer(int maxOdometer)
        {
            var cars = _context.Cars.Where(c => c.Status == "available" && c.OdometerReading <= maxOdometer).ToList();
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.InventoryNumber} - {car.Make} {car.Model}, {car.Year}, ${car.Price}");
            }
        }

        public void ListCarsByMakeAndModel(string make, string model)
        {
            var cars = _context.Cars.Where(c => c.Status == "available" && c.Make == make && c.Model == model).ToList();
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.InventoryNumber} - {car.Make} {car.Model}, {car.Year}, ${car.Price}");
            }
        }

        public void ListCarsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var cars = _context.Cars.Where(c => c.Status == "available" && c.Price >= minPrice && c.Price <= maxPrice).ToList();
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.InventoryNumber} - {car.Make} {car.Model}, {car.Year}, ${car.Price}");
            }
        }

        public void ChangeCarPrice(string inventoryNumber, decimal newPrice)
        {
            var car = _context.Cars.FirstOrDefault(c => c.InventoryNumber == inventoryNumber);
            if (car != null)
            {
                car.Price = newPrice;
                _context.SaveChanges();
                Console.WriteLine("Price updated successfully.");
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }

        public void DeleteCar(string inventoryNumber)
        {
            var car = _context.Cars.FirstOrDefault(c => c.InventoryNumber == inventoryNumber);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                Console.WriteLine("Car deleted successfully.");
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }
    }
}
