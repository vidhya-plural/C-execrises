using System;
using System.Collections.Generic;
using System.IO;

namespace PetStore
{
    // Enum definitions for FoodType and AnimalType
    public enum FoodType
    {
        Dry,
        Wet
    }

    public enum AnimalType
    {
        Dog,
        Cat,
        Bird,
        Fish,
        Reptile,
        SmallAnimal
    }

    // Base class for all inventory items
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // Derived class for Food items
    public class FoodItem : InventoryItem
    {
        public string Brand { get; set; }
        public FoodType FoodType { get; set; }
        public AnimalType AnimalType { get; set; }
    }

    // Derived class for Accessory items
    public class AccessoryItem : InventoryItem
    {
        public string Size { get; set; }
        public string Color { get; set; }
    }

    // Derived class for Cage items
    public class CageItem : InventoryItem
    {
        public string Dimensions { get; set; }
        public string Material { get; set; }
    }

    // Derived class for Aquarium items
    public class AquariumItem : InventoryItem
    {
        public int Capacity { get; set; }
        public string Shape { get; set; }
    }

    // Derived class for Toy items
    public class ToyItem : InventoryItem
    {
        public string Material { get; set; }
        public string RecommendedAge { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to store inventory items
            List<InventoryItem> inventory = new List<InventoryItem>();

            // Load data from a file
            LoadInventoryFromFile("D:\\Users\\vidhya\\Desktop\\csharp\\C-execrises\\inventory.txt", inventory);

            // Display loaded inventory items
            DisplayInventory(inventory);
        }

        // Method to load inventory data from a file
        static void LoadInventoryFromFile(string fileName, List<InventoryItem> inventory)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 9)
                        {
                            // Parse fields from the line
                            int id = int.Parse(parts[0]);
                            string category = parts[1];
                            string name = parts[2];
                            string description = parts[3];
                            decimal price = decimal.Parse(parts[4]);
                            int quantity = int.Parse(parts[5]);

                            // Create inventory item based on category
                            InventoryItem item;
                            switch (category)
                            {
                                case "Food":
                                    item = new FoodItem
                                    {
                                        Id = id,
                                        Category = category,
                                        Name = name,
                                        Description = description,
                                        Price = price,
                                        Quantity = quantity,
                                        Brand = parts[6],
                                        FoodType = (FoodType)Enum.Parse(typeof(FoodType), parts[7]),
                                        AnimalType = (AnimalType)Enum.Parse(typeof(AnimalType), parts[8])
                                    };
                                    break;
                                case "Accessory":
                                    item = new AccessoryItem
                                    {
                                        Id = id,
                                        Category = category,
                                        Name = name,
                                        Description = description,
                                        Price = price,
                                        Quantity = quantity,
                                        Size = parts[6],
                                        Color = parts[7]
                                    };
                                    break;
                                case "Cage":
                                    item = new CageItem
                                    {
                                        Id = id,
                                        Category = category,
                                        Name = name,
                                        Description = description,
                                        Price = price,
                                        Quantity = quantity,
                                        Dimensions = parts[6],
                                        Material = parts[7]
                                    };
                                    break;
                                case "Aquarium":
                                    item = new AquariumItem
                                    {
                                        Id = id,
                                        Category = category,
                                        Name = name,
                                        Description = description,
                                        Price = price,
                                        Quantity = quantity,
                                        Capacity = int.Parse(parts[6]),
                                        Shape = parts[7]
                                    };
                                    break;
                                case "Toy":
                                    item = new ToyItem
                                    {
                                        Id = id,
                                        Category = category,
                                        Name = name,
                                        Description = description,
                                        Price = price,
                                        Quantity = quantity,
                                        Material = parts[6],
                                        RecommendedAge = parts[7]
                                    };
                                    break;
                                default:
                                    Console.WriteLine($"Unknown category: {category}. Skipping item.");
                                    continue;
                            }

                            // Add item to inventory list
                            inventory.Add(item);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid line format: {line}. Skipping.");
                        }
                    }
                }

                Console.WriteLine("Inventory loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading inventory: {ex.Message}");
            }
        }

        // Method to display all inventory items
        static void DisplayInventory(List<InventoryItem> inventory)
        {
            Console.WriteLine("\nInventory Items:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"ID: {item.Id}");
                Console.WriteLine($"Category: {item.Category}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: ${item.Price}");
                Console.WriteLine($"Quantity: {item.Quantity}");

                // Specific details based on item type
                if (item is FoodItem foodItem)
                {
                    Console.WriteLine($"Brand: {foodItem.Brand}");
                    Console.WriteLine($"Food Type: {foodItem.FoodType}");
                    Console.WriteLine($"Animal Type: {foodItem.AnimalType}");
                }
                else if (item is AccessoryItem accessoryItem)
                {
                    Console.WriteLine($"Size: {accessoryItem.Size}");
                    Console.WriteLine($"Color: {accessoryItem.Color}");
                }
                else if (item is CageItem cageItem)
                {
                    Console.WriteLine($"Dimensions: {cageItem.Dimensions}");
                    Console.WriteLine($"Material: {cageItem.Material}");
                }
                else if (item is AquariumItem aquariumItem)
                {
                    Console.WriteLine($"Capacity: {aquariumItem.Capacity} gallons");
                    Console.WriteLine($"Shape: {aquariumItem.Shape}");
                }
                else if (item is ToyItem toyItem)
                {
                    Console.WriteLine($"Material: {toyItem.Material}");
                    Console.WriteLine($"Recommended Age: {toyItem.RecommendedAge}");
                }

                Console.WriteLine();
            }
        }
    }
}
