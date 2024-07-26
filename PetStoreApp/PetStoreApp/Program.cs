using System;
using PetStoreApp.Models;

namespace PetStoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new InventoryManager();
            string filePath = "D:\\Users\\vidhya\\Desktop\\csharp\\PetStoreApp\\PetStoreApp\\Data\\inventory.txt";

            // Load inventory from file
            manager.LoadInventoryFromFile(filePath);

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Show all items");
                Console.WriteLine("2 - Show item details");
                Console.WriteLine("3 - Purchase item");
                Console.WriteLine("4 - Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.ShowAllItems();
                        break;
                    case "2":
                        Console.Write("Enter item ID: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            manager.ShowItemDetails(id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter item ID to purchase: ");
                        if (int.TryParse(Console.ReadLine(), out int purchaseId))
                        {
                            manager.PurchaseItem(purchaseId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID.");
                        }
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
