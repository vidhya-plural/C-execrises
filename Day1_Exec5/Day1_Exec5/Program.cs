using System;

namespace PriceQuoter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Display the product codes and their respective prices
            Console.WriteLine("Product Codes:");
            Console.WriteLine("BG-127  - Quantity 1-24: $18.99, Quantity 25-50: $17.00, Quantity 51+: $14.49");
            Console.WriteLine("WRTR-28 - Quantity 1-24: $125.00, Quantity 25-50: $113.75, Quantity 51+: $99.99");
            Console.WriteLine("GUAC-8  - Quantity 1-24: $8.99, Quantity 25-50: $8.99, Quantity 51+: $7.49");
            Console.WriteLine();

            // Prompt user for product code
            Console.Write("Enter the product code: ");
            string productCode = Console.ReadLine().Trim().ToUpper();

            // Prompt user for quantity
            Console.Write("Enter the quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            // Determine price based on product code and quantity
            decimal unitPrice = 0;
            decimal totalPrice = 0;

            switch (productCode)
            {
                case "BG-127":
                    if (quantity >= 1 && quantity <= 24)
                        unitPrice = 18.99m;
                    else if (quantity >= 25 && quantity <= 50)
                        unitPrice = 17.00m;
                    else if (quantity >= 51)
                        unitPrice = 14.49m;
                    break;
                case "WRTR-28":
                    if (quantity >= 1 && quantity <= 24)
                        unitPrice = 125.00m;
                    else if (quantity >= 25 && quantity <= 50)
                        unitPrice = 113.75m;
                    else if (quantity >= 51)
                        unitPrice = 99.99m;
                    break;
                case "GUAC-8":
                    if (quantity >= 1 && quantity <= 24)
                        unitPrice = 8.99m;
                    else if (quantity >= 25 && quantity <= 50)
                        unitPrice = 8.99m; // Same price for 25-50 units
                    else if (quantity >= 51)
                        unitPrice = 7.49m;
                    break;
                default:
                    Console.WriteLine("Invalid product code entered.");
                    return;
            }

            // Calculate total price
            totalPrice = quantity * unitPrice;

            // Apply additional discount for large orders (250 units or more)
            if (quantity >= 250)
            {
                decimal discountAmount = totalPrice * 0.15m; // 15% discount
                decimal discountedPrice = totalPrice - discountAmount;

                Console.WriteLine($"Large order discount applied (15% off): -${discountAmount:F2}");
                Console.WriteLine($"Total price (after discount): ${discountedPrice:F2}");
            }
            else
            {
                Console.WriteLine($"Total price: ${totalPrice:F2}");
            }
        }
    }
}
