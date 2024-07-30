using System;
using System.IO;
using System.Linq;
using Dealership.Data;
using Dealership.Models;

namespace Dealership
{
    public class SaleService
    {
        private readonly DealershipContext _context;

        public SaleService(DealershipContext context)
        {
            _context = context;
        }

        public void SellCar(string inventoryNumber)
        {
            var car = _context.Cars.FirstOrDefault(c => c.InventoryNumber == inventoryNumber && c.Status == "available");
            if (car != null)
            {
                Console.WriteLine($"Selling Car: {car.InventoryNumber} - {car.Make} {car.Model}, {car.Year}, ${car.Price}");

                Console.Write("Enter customer name: ");
                string customerName = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter customer phone: ");
                string customerPhone = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter payment method: ");
                string paymentMethod = Console.ReadLine() ?? string.Empty;
                Console.Write("Enter payment amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal paymentAmount))
                {
                    Console.WriteLine("Invalid payment amount.");
                    return;
                }

                var sale = new Sale
                {
                    InventoryNumber = car.InventoryNumber,
                    SalesDate = DateTime.Now,
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    PaymentMethod = paymentMethod,
                    PaymentAmount = paymentAmount,
                    CarId = car.Id // Use the CarId for the foreign key
                };

                // Add sale to the context
                _context.Sales.Add(sale);

                // Update car status
                car.Status = "sold";

                // Save changes to the database
                _context.SaveChanges();

                // Create receipt file
                string receiptFileName = $"{car.InventoryNumber}_receipt.txt";
                using (var writer = new StreamWriter(receiptFileName))
                {
                    writer.WriteLine("RECEIPT");
                    writer.WriteLine($"Inventory Number: {car.InventoryNumber}");
                    writer.WriteLine($"Make: {car.Make}");
                    writer.WriteLine($"Model: {car.Model}");
                    writer.WriteLine($"Year: {car.Year}");
                    writer.WriteLine($"Price: ${car.Price}");
                    writer.WriteLine($"Sales Date: {sale.SalesDate}");
                    writer.WriteLine($"Customer Name: {sale.CustomerName}");
                    writer.WriteLine($"Customer Phone: {sale.CustomerPhone}");
                    writer.WriteLine($"Payment Method: {sale.PaymentMethod}");
                    writer.WriteLine($"Payment Amount: ${sale.PaymentAmount}");
                }

                Console.WriteLine("Sale recorded and receipt created.");
            }
            else
            {
                Console.WriteLine("Car not found or already sold.");
            }
        }
    }
}
